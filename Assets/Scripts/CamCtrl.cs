using UnityEngine;

public class CamCtrl : MonoBehaviour
{
    [Header("Look")]
    public float lookSensitivity = 2.0f;        // 마우스 감도
    public Vector2 pitchClamp = new Vector2(-80f , 80f);
    public bool invertY = false;                // 상하 반전

    [Header("Move")]
    public float moveSpeed = 5f;                // 기본 이동 속도 (m/s)
    public float fastMultiplier = 3f;           // Shift 가속 배수
    public float slowMultiplier = 0.4f;         // Ctrl 감속 배수
    public bool enableVerticalQE = true;        // Q/E로 수직 이동 허용

    float yaw;   // Y축
    float pitch; // X축
    bool rotating;

    void Start()
    {
        // 현재 초기 회전값을 저장해 두면 시작 각도가 보존됨
        Vector3 e = transform.eulerAngles;
        yaw = e.y;
        pitch = e.x;
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    void HandleRotation()
    {
        // RMB 누르는 동안만 회전
        if (Input.GetMouseButtonDown(1))
        {
            rotating = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            rotating = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (!rotating)
            return;

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        float invert = invertY ? 1f : -1f; // 일반적으로 위로 올리면 pitch 감소(-)
        yaw += mouseX * lookSensitivity;
        pitch += mouseY * lookSensitivity * invert;

        pitch = Mathf.Clamp(pitch , pitchClamp.x , pitchClamp.y);

        // Roll(좌우 기울기) 없이 Yaw/Pitch만 적용
        transform.rotation = Quaternion.Euler(pitch , yaw , 0f);
    }

    void HandleMovement()
    {
        // 입력(WSAD)
        float h = (Input.GetKey(KeyCode.A) ? -1f : 0f) + (Input.GetKey(KeyCode.D) ? 1f : 0f);
        float v = (Input.GetKey(KeyCode.S) ? -1f : 0f) + (Input.GetKey(KeyCode.W) ? 1f : 0f);

        // 수직 이동(Q/E)
        float y = 0f;
        if (enableVerticalQE)
        {
            y = (Input.GetKey(KeyCode.Q) ? -1f : 0f) + (Input.GetKey(KeyCode.E) ? 1f : 0f);
        }

        Vector3 inputDir = new Vector3(h , y , v);
        if (inputDir.sqrMagnitude > 1f)
            inputDir.Normalize();

        // 속도 배수 (Shift 가속 / Ctrl 감속)
        float speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            speed *= fastMultiplier;
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            speed *= slowMultiplier;

        // 로컬 기준 이동
        Vector3 worldMove = transform.TransformDirection(inputDir) * speed * Time.deltaTime;
        transform.position += worldMove;
    }
}

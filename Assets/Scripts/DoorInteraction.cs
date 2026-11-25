using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class DoorInteraction : MonoBehaviour
{
    public enum DoorState
    {
        Left,
        Right,
        None
    }
    public DoorState state;
    public Transform rootBone;  // SkinnedMeshRenderer의 rootBone을 Drag&Drop
    public Vector3 openRotation = new Vector3(0 , 90 , 0); // 문 열릴 각도
    public float openSpeed = 1.5f; // 열리는 속도
    private bool isOpened = false;
    public GameObject myFire;

    private void Start()
    {
        if (rootBone == null)
        {
            rootBone = GetComponent<SkinnedMeshRenderer>().rootBone;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOpened)
            return;
        if (other.CompareTag("Hand")) // VR 컨트롤러에 이 태그 붙여줘
        {
            isOpened = true;
            StartCoroutine(OpenDoor());
            GetComponent<NavMeshObstacle>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private IEnumerator OpenDoor()
    {
        Quaternion startRot = rootBone.localRotation;
        Quaternion endRot = Quaternion.Euler(openRotation);
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            rootBone.localRotation = Quaternion.Slerp(startRot , endRot , t);            
            yield return null;
        }
        Player player = FindAnyObjectByType<Player>();
        switch (state)
        {
            case DoorState.Left:
                if (player.state == Player.State.Right && player._level == Player.Level.Hard)
                {
                    myFire.SetActive(true);
                    Destroy(myFire , 3);
                }
                break;
            case DoorState.Right:
                if (player.state == Player.State.Left && player._level == Player.Level.Hard)
                {
                    myFire.SetActive(true);
                    Destroy(myFire , 3);
                }
                break;
        }
       
        rootBone.localRotation = endRot; // 보정
    }


}

using UnityEngine;

public class BackendManager : MonoBehaviour
{
    public static BackendManager Instance
    {
        get; private set;
    }

    public int level;

    void Awake()
    {
        // 이미 존재하면 새로 생성된 객체는 파괴
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 이동 시 유지
    }

    // 예시: 데이터 초기화용 함수
    public void Init()
    {
        Debug.Log("BackendManager Initialized");
    }
}

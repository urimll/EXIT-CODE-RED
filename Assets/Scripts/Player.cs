using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public enum State
    {
        Left,
        Right
    }
    public State state;

    public enum Level
    {
        Easy,
        Hard
    }
    public Level _level;
    public bool PinOut = false;
    public int CheckState = 0;

    public Animator PinAni;

    public GameObject questCanvas;
    public GameObject indexCanvas;
    public GameObject goalCanvas;
    public GameObject overCanvas;
    public string[] indexString;
    public TMP_Text indexText;
    int indexNum;
    public GameObject fireBottle;
    public GameObject fires;
    public GameObject startFire;
    public GameObject[] questFire;
    public GameObject[] bigFire;
    public GameObject[] questBottles;
    public GameObject questBottle_Hard;
    public SkinnedMeshRenderer[] handles1;
    public SkinnedMeshRenderer[] handles2;

    public GameObject arrow;    
    public GameObject hp;
    public Image hpBar;
    float interval;

    public NavMeshAgent agent;
    public Transform head;
    public float moveSpeed = 2.5f;
    public float rotateSpeed = 90f;
    public float sampleRange = 1f;
    public float maxStepHeight = 0.5f;
    Rigidbody rb; Vector3 pendingMove; float pendingYaw;

    float ShotTime = 0;
    float CheckTime = 0;
    public string hand;
    public Transform ShotPoint;
    public GameObject ShotEffectObject;
    bool isStart;
    BackendManager _backendManager;
    private void Start()
    {
        _backendManager = FindAnyObjectByType<BackendManager>();
        isStart = true;
        int num = Random.Range(0 , 2);
        transform.GetChild(0).transform.localPosition = Vector3.up * 1.4f;
        state = (State)num;
        //state = State.Left;
        _level = (Level)_backendManager.level;
        //_level = Level.Hard;           
        
        switch (_level)
        {
            case Level.Easy:
                arrow.SetActive(true);
                hp.SetActive(false);
                questBottle_Hard.SetActive(false);
                startFire.SetActive(false);
                for (int i = 0; i < questFire.Length; i++)
                {
                    questFire[i].SetActive(true);
                }
                break;

            case Level.Hard:
                switch (state)
                {
                    case State.Left:
                        for (int i = 0; i < handles2.Length; i++)
                        {
                            Material mat = handles2[i].material;
                            mat.color = new Color(180f / 255 , 76f / 255 , 0);
                            mat.EnableKeyword("_EMISSION");
                        }
                        break;

                    case State.Right:
                        for (int i = 0; i < handles1.Length; i++)
                        {
                            Material mat = handles1[i].material;
                            mat.color = new Color(180f / 255 , 76f / 255 , 0);
                            mat.EnableKeyword("_EMISSION");
                        }
                        break;
                }
                questFire[num].SetActive(true);
                bigFire[num].SetActive(true);
                arrow.SetActive(false);
                hp.SetActive(true);
                for (int i = 0; i < questBottles.Length; i++)
                {
                    questBottles[i].GetComponent<BoxCollider>().enabled = false;
                }
                break;
        }

        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.baseOffset = 0f;
        agent.Warp(transform.position);   // ���� ����ȭ
    }

    void Update()
    {
        if (isStart)
        {
            if (_level == Level.Hard)
            {
                interval += Time.deltaTime;
                hpBar.fillAmount = (100 - interval) / 100;
                if (hpBar.fillAmount <= 0.5f && hpBar.fillAmount > 0.2f)
                {
                    hpBar.color = Color.yellow;
                }

                else if (hpBar.fillAmount <= 0.2f)
                {
                    hpBar.color = Color.red;
                }
                if (hpBar.fillAmount < 0.01f)
                {
                    GameOver(false);
                }
            }

            Move();
            Shot();
        }        
    }
    void Move()
    {
        // 오른손 스틱: 이동
        Vector2 r = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick , OVRInput.Controller.RTouch);
        Vector3 dir = head.forward * r.y + head.right * r.x;
        dir.y = 0f;

        Vector3 cur = transform.position;
        Vector3 next = cur + dir.normalized * moveSpeed * Time.deltaTime;

        if (NavMesh.SamplePosition(next , out NavMeshHit hit , sampleRange , NavMesh.AllAreas))
        {
            next.x = hit.position.x;
            next.z = hit.position.z;

            float dy = hit.position.y - cur.y;
            if ((dy > 0f && dy <= maxStepHeight) || (dy < 0f && -dy <= maxStepHeight))
                next.y = hit.position.y;
            // 큰 단차는 유지하고 싶으면:
            // next.y = Mathf.MoveTowards(cur.y, hit.position.y, stepLerpSpeed * Time.deltaTime);
        }

        transform.position = next;

        // 왼손 스틱 좌/우: 회전
        Vector2 l = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick , OVRInput.Controller.LTouch);
        transform.Rotate(Vector3.up , l.x * rotateSpeed * Time.deltaTime);
    }

    void Shot()
    {
        switch (CheckState)
        {
            case 4:
                switch (hand)
                {
                    case "Right":
                        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger , OVRInput.Controller.RTouch))
                        {
                            PinAni.SetInteger("FireAniCheck" , 2);
                            CheckState = 5;
                        }
                        break;

                    case "Left":
                        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger , OVRInput.Controller.LTouch))
                        {
                            PinAni.SetInteger("FireAniCheck" , 2);
                            CheckState = 5;
                        }
                        break;
                }
               
                break;
            case 5:
                ShotEffect();

                switch (hand)
                {
                    case "Right":
                        if (!OVRInput.Get(OVRInput.Button.PrimaryHandTrigger , OVRInput.Controller.RTouch))
                        {
                            PinAni.SetInteger("FireAniCheck" , 3);
                            CheckTime = Time.time + 0.5f;
                            CheckState = 6;
                        }
                        break;

                    case "Left":
                        if (!OVRInput.Get(OVRInput.Button.PrimaryHandTrigger , OVRInput.Controller.LTouch))
                        {
                            PinAni.SetInteger("FireAniCheck" , 3);
                            CheckTime = Time.time + 0.5f;
                            CheckState = 6;
                        }
                        break;
                }
                
                break;
            case 6:
                if (CheckTime < Time.time)
                {
                    PinAni.SetInteger("FireAniCheck" , 4);
                    CheckState = 4;
                }
                break;
        }
    }

    void ShotEffect()
    {
        if (ShotTime > Time.time)
            return;

        GameObject CreateEff = Instantiate(ShotEffectObject , ShotPoint.position , ShotPoint.rotation);
        Rigidbody rigid = CreateEff.GetComponent<Rigidbody>();
        rigid.linearVelocity = ShotPoint.forward * 10;
        ShotTime = Time.time + 0.3f;

        Destroy(CreateEff , 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Quest"))
        {
            if (_level == Level.Hard)
            {
                QuestStartButtonClick();
            }

            else
            {
                questCanvas.SetActive(true);
            }            
            other.enabled = false;           
        }

        if (other.CompareTag("Finish"))
        {            
            GameOver(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("FireQuest"))
        {
            interval += Time.deltaTime;
            interval += Time.deltaTime;
            interval += Time.deltaTime;
            interval += Time.deltaTime;
        }

        if (collision.gameObject.CompareTag("BigFire"))
        {
            interval += Time.deltaTime;
            interval += Time.deltaTime;
            interval += Time.deltaTime;
            interval += Time.deltaTime;
        }
    }

    void GameOver(bool tf)
    {
        goalCanvas.SetActive(tf);
        overCanvas.SetActive(!tf);
        isStart = false;
        goalCanvas.transform.GetChild((int)_level).gameObject.SetActive(true);
    }

    public void LoadScene(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void PlayHardMode()
    {
        _backendManager.level = 1;
        SceneManager.LoadScene(1);
    }

    #region UI
    public void QuestStartButtonClick()
    {
        questCanvas.SetActive(false);
        fireBottle.SetActive(true);
        indexCanvas.SetActive(_level == Level.Easy);
        indexText.text = indexString[indexNum];
        for (int i = 0; i < questBottles.Length; i++)
        {
            questBottles[i].SetActive(false);
        }
        questBottle_Hard.SetActive(false);
    }

    public void NextQuest()
    {
        indexNum++;
        if (indexNum == indexString.Length)
        {
            indexCanvas.SetActive(false);
            return;
        }
        indexText.text = indexString[indexNum];
    }
    #endregion
}

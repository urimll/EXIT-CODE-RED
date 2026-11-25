using System.Collections;
using UnityEngine;

public class QuestFire : MonoBehaviour
{
    public Transform[] DonSizeObject;
    public Transform[] SizeChangeObject;
    public Player _player;

    public float SizeOf = 0.1f;

    public bool HitCheck = false;

    public bool ObjectEnd = false;

    float ChangeSize;

    public bool CantFire;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeSize = SizeOf / 2;
    }

    // Update is called once per frame
    void Update()
    {
        HitObjectSizeChange();
        if (ObjectEnd)
        {
            this.enabled = false;
        }
    }

    void HitObjectSizeChange()
    {
        if (!HitCheck)
            return;

        int ObjCount = SizeChangeObject.Length;
        for (int i = 0; i < ObjCount; i++)
        {
            Vector3 vecSize = SizeChangeObject[i].transform.localScale;
            if (vecSize.x > 0 || vecSize.y > 0 || vecSize.z > 0)
            {
                vecSize.x -= ChangeSize;
                vecSize.y -= ChangeSize;
                vecSize.z -= ChangeSize;
                if (CantFire)
                {
                    if (vecSize.x < 0.5f)
                    {
                        StartCoroutine(ScaleUp(SizeChangeObject[i].transform , 1.5f , 0.1f));
                    }
                }
                SizeChangeObject[i].transform.localScale = vecSize;
            }
            else
            {
                SizeChangeObject[i].gameObject.SetActive(false);
                ObjectEnd = true;
                if(_player._level == Player.Level.Easy)
                {
                    _player.NextQuest();
                    _player.fireBottle.SetActive(false);
                    _player.CheckState = 7;                 
                }
            }
        }
        HitCheck = false;
    }

    IEnumerator ScaleUp(Transform t , float target , float time)
    {
        Vector3 start = t.localScale;
        Vector3 end = Vector3.one * target;
        float elapsed = 0f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            t.localScale = Vector3.Lerp(start , end , elapsed / time);
            yield return null;
        }
        t.localScale = end;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            if (CantFire && SizeChangeObject[0].localScale.x < 0.5f)
            {
                return;
            }
            HitCheck = true;
        }
    }
}

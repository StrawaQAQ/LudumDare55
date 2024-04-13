using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlyWord : MonoBehaviour
{
    private RectTransform rectTransform;
    public TextMeshProUGUI text;
    public Vector2 yRange;
    public float speed;
    public float deltaY;
    private bool CanFly;
    private TimerClass selfTimer=new TimerClass();


    private static Dictionary<int, TimerClass> PosTimer = new();
    private static int index = 0;
    private void Awake()
    {
        selfTimer.ResetDeltaTime(1000000000000000);
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-10000, 0);
    }
    private void FixedUpdate()
    {
        if (selfTimer.finishTimer)
        {
            CanFly=false;
            selfTimer.ResetDeltaTime(1000000000000000);
            text.text = "";
            rectTransform.anchoredPosition = new Vector2(-10000,0);
            CachePool.Destroy(gameObject);
           
            //返回
        }
        if(CanFly)rectTransform.anchoredPosition -= new Vector2(speed*Time.deltaTime, 0);
    }
    public void Fly(string word)
    {
        text.text = word;
        StartCoroutine(Fly_(word));

    }

    private IEnumerator Fly_(string word)
    {
        yield return new WaitForSeconds(0.1f);
        index = 0;
        
    again:
        if (!PosTimer.ContainsKey(index))
        {
            PosTimer.Add(index, new TimerClass());
            float deltaTime =rectTransform.rect.width/ speed+0.1f;
            PosTimer[index].ResetDeltaTime(deltaTime);
            PosTimer[index].OpenTimer();
            rectTransform.anchoredPosition = new Vector2(0, yRange.y - index * deltaY);
        }
        else
        {
            if (PosTimer[index].finishTimer)
            {
                float deltaTime = rectTransform.rect.width / speed+0.1f;
                PosTimer[index].ResetDeltaTime(deltaTime);
                PosTimer[index].OpenTimer();
                rectTransform.anchoredPosition = new Vector2(0, yRange.y - index * deltaY);
            }
            else
            {
                if (PosTimer.Count == Mathf.Floor(yRange.GetLength() / deltaY))
                {
                    foreach (int a in PosTimer.Keys)
                    {
                        if (PosTimer[a].finishTimer)
                        {
                            index = a;
                            index = index > yRange.GetLength() / deltaY ? 0 : index;
                            goto again;
                        }

                    }
                    Destroy(gameObject);
                    yield break;

                }else
                {
                    index++;
                    goto again;

                }
             

            }
        }

        float waitTime = (transform.parent.GetComponent<RectTransform>().rect.width + rectTransform.rect.width) / speed;
        selfTimer.ResetDeltaTime(waitTime);
        selfTimer.OpenTimer();
        CanFly = true;


        index++;
        index = index > yRange.GetLength() / deltaY ? 0 : index;
    }

}

public static class ExtendFunc
{

    public static float GetLength(this Vector2 value_)
    {
        return value_.y - value_.x;
    }


}


[System.Serializable]
public class TimerClass
{
    [SerializeField][Range(0.1f, 100)] private float deltaTime;
    /// <summary>
    /// true表示间隔时间结束，false表示还在计时
    /// </summary>

    [HideInInspector]
    public bool finishTimer
    {
        get
        {
            return Timer + deltaTime < Time.time;
        }
        set
        {
            if (value)
            {
                Timer = Time.time;
            }
        }
    }

    /// <summary>
    /// 开启计时器
    /// </summary>
    public void OpenTimer()
    {
        Timer = Time.time;
    }
    /// <summary>
    /// 手动关闭计时器
    /// </summary>
    public void CloseTimer()
    {
        Timer = -1000000;
    }
    /// <summary>
    /// 不会重置计算
    /// </summary>
    /// <param name="newDeltaTime"></param>
    public void ResetDeltaTime(float newDeltaTime)
    {
        deltaTime = newDeltaTime;
    }

    private float Timer = -100000;

}

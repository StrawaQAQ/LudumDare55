using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyboradListen : MonoBehaviour
{
    [Header("按键列表")]
    public List<GameObject> keys; // 按键列表  
    private List<ShowKey> showKeys; // ShowKey组件列表  
    private List<float> correctSequence; // 正确的按键顺序  
    private List<float> inputSequence = new List<float>(); // 玩家的输入序列  
    private bool isSequenceStarted = false; // 标记是否已经开始检查序列  
    private PlayerControl pc;
    private int i;
    [Header("qte限制时间")]
    public float MaxTime;
    public float escapTime;
    [Header("最大Key值")]
    public int MaxKey = 3;
    [Header("获取游戏监听器")]
    public GameObject GameListenner;
    private GameListenner GL;
    [Header("Key值增加时间间隔")]
    public float BtwTime;
    [HideInInspector]public float tempTime;
    void Start()
    {
        showKeys = new List<ShowKey>();
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        GL = GameListenner.GetComponent<GameListenner>();
        MaxKey = 3;
        tempTime = BtwTime;
        i = 0;
        escapTime = 0f;
        foreach (var key in keys)
        {
            showKeys.Add(key.GetComponent<ShowKey>());
        }
        correctSequence = new List<float>();
        // 假设 ShowKey 组件的 num 属性是按键的正确顺序 
        Debug.Log(keys.Count);
    }

    void Update()
    {
        if (!isSequenceStarted && Input.anyKeyDown && pc.enableCall && pc.getNum >= GL.MaxKey)
        {
            isSequenceStarted = true;
            pc.getNum -= MaxKey;
        }

        if (isSequenceStarted)
        {
            
            KeyTag();
            //Debug.Log(inputSequence.Count);
            if (i >= MaxKey)
            {
                Check();
            }
        }
        if (MaxKey <= 7 && !pc.enableCall)
        {
            TimeListenner();
        }
    }
    private void FixedUpdate()
    {
        Timelimit();
    }

    void KeyTag()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
           inputSequence.Add(1);
            correctSequence.Add(showKeys[i].num);
            if (inputSequence[i] == correctSequence[i])
            {
               // Debug.Log("按键1被按下，添加到序列中。");
               // Debug.Log(correctSequence.Count);
                i++;
            }
            else
            {
                Debug.Log("玩家没有正确地按顺序按键。");
                ResetSequence();
            }
                
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            inputSequence.Add(2);
            correctSequence.Add(showKeys[i].num);
            if (inputSequence[i] == correctSequence[i])
            {
               // Debug.Log("按键2被按下，添加到序列中。");
               // Debug.Log(correctSequence.Count);
                i++;
            }
            else
            { 
                Debug.Log("玩家没有正确地按顺序按键。");
                ResetSequence();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            inputSequence.Add(3);
            correctSequence.Add(showKeys[i].num);
            if (inputSequence[i] == correctSequence[i])
            {
               // Debug.Log("按键3被按下，添加到序列中。");
               // Debug.Log(correctSequence.Count);
                i++;
            }
            else
            {
                Debug.Log("玩家没有正确地按顺序按键。");
                ResetSequence();
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            inputSequence.Add(4);
            correctSequence.Add(showKeys[i].num);
            if (inputSequence[i] == correctSequence[i])
            {
               // Debug.Log("按键4被按下，添加到序列中。");
               // Debug.Log(correctSequence.Count);
                i++;
            }
            else
            {
                Debug.Log("玩家没有正确地按顺序按键。");
                ResetSequence();
            }
        }
    }

    void Check()
    {
        
        if (inputSequence.SequenceEqual(correctSequence))
        {
            Debug.Log("玩家正确地按顺序按键了！");
            BulletChatController.instance.AddBulletChat(name, "睿睿你是大明星");
            ResetSequence();
        }
        else
        {
            Debug.Log("玩家没有正确地按顺序按键。");
            ResetSequence();
        }
    }

    void ResetSequence()
    {
        isSequenceStarted = false;
        inputSequence.Clear();
        correctSequence.Clear();
        pc.enableCall = false;
        pc.enableMove = true;
        escapTime = 0f;
        gameObject.SetActive(false);
        i = 0;
    }
    void Timelimit()
    {
        if (escapTime >= MaxTime)
        {
            ResetSequence();
            Debug.Log("YES");
        }
        else
        {
            escapTime += Time.deltaTime;

        }
    }
    void TimeListenner()
    {
        if(GL.GameTime >= tempTime)
        {
            keys[MaxKey].SetActive(true);
            MaxKey += 1;
            tempTime += BtwTime;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyboradListen : MonoBehaviour
{
    public List<GameObject> keys; // 按键列表  
    private List<ShowKey> showKeys; // ShowKey组件列表  
    private List<float> correctSequence; // 正确的按键顺序  
    private List<float> inputSequence = new List<float>(); // 玩家的输入序列  
    private bool isSequenceStarted = false; // 标记是否已经开始检查序列  
    private PlayerControl pc;
    private int i;
    void Start()
    {
        showKeys = new List<ShowKey>();
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        i = 0;
        foreach (var key in keys)
        {
            showKeys.Add(key.GetComponent<ShowKey>());
        }
        correctSequence = new List<float>();
        // 假设 ShowKey 组件的 num 属性是按键的正确顺序 
    }

    void Update()
    {
        if (!isSequenceStarted && Input.anyKeyDown && pc.enableCall)
        {
            isSequenceStarted = true;
        }

        if (isSequenceStarted)
        {
            KeyTag();
            //Debug.Log(inputSequence.Count);
            if (inputSequence.Count >= 4)
            {
                Check();
            }
        }
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
        gameObject.SetActive(false);
        i = 0;
    }
}
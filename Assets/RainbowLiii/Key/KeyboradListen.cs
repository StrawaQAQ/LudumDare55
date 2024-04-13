using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyboradListen : MonoBehaviour
{
    public List<GameObject> keys; // �����б�  
    private List<ShowKey> showKeys; // ShowKey����б�  
    private List<float> correctSequence; // ��ȷ�İ���˳��  
    private List<float> inputSequence = new List<float>(); // ��ҵ���������  
    private bool isSequenceStarted = false; // ����Ƿ��Ѿ���ʼ�������  
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
        // ���� ShowKey ����� num �����ǰ�������ȷ˳�� 
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
               // Debug.Log("����1�����£���ӵ������С�");
               // Debug.Log(correctSequence.Count);
                i++;
            }
            else
            {
                Debug.Log("���û����ȷ�ذ�˳�򰴼���");
                ResetSequence();
            }
                
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            inputSequence.Add(2);
            correctSequence.Add(showKeys[i].num);
            if (inputSequence[i] == correctSequence[i])
            {
               // Debug.Log("����2�����£���ӵ������С�");
               // Debug.Log(correctSequence.Count);
                i++;
            }
            else
            { 
                Debug.Log("���û����ȷ�ذ�˳�򰴼���");
                ResetSequence();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            inputSequence.Add(3);
            correctSequence.Add(showKeys[i].num);
            if (inputSequence[i] == correctSequence[i])
            {
               // Debug.Log("����3�����£���ӵ������С�");
               // Debug.Log(correctSequence.Count);
                i++;
            }
            else
            {
                Debug.Log("���û����ȷ�ذ�˳�򰴼���");
                ResetSequence();
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            inputSequence.Add(4);
            correctSequence.Add(showKeys[i].num);
            if (inputSequence[i] == correctSequence[i])
            {
               // Debug.Log("����4�����£���ӵ������С�");
               // Debug.Log(correctSequence.Count);
                i++;
            }
            else
            {
                Debug.Log("���û����ȷ�ذ�˳�򰴼���");
                ResetSequence();
            }
        }
    }

    void Check()
    {
        
        if (inputSequence.SequenceEqual(correctSequence))
        {
            Debug.Log("�����ȷ�ذ�˳�򰴼��ˣ�");
            ResetSequence();
        }
        else
        {
            Debug.Log("���û����ȷ�ذ�˳�򰴼���");
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
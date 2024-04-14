using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyboradListen : MonoBehaviour
{
    [Header("�����б�")]
    public List<GameObject> keys; // �����б�  
    private List<ShowKey> showKeys; // ShowKey����б�  
    private List<float> correctSequence; // ��ȷ�İ���˳��  
    private List<float> inputSequence = new List<float>(); // ��ҵ���������  
    private bool isSequenceStarted = false; // ����Ƿ��Ѿ���ʼ�������  
    private PlayerControl pc;
    private int i;
    [Header("qte����ʱ��")]
    public float MaxTime;
    public float escapTime;
    [Header("���Keyֵ")]
    public int MaxKey = 3;
    [Header("��ȡ��Ϸ������")]
    public GameObject GameListenner;
    private GameListenner GL;
    [Header("Keyֵ����ʱ����")]
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
        // ���� ShowKey ����� num �����ǰ�������ȷ˳�� 
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
            BulletChatController.instance.AddBulletChat(name, "�����Ǵ�����");
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
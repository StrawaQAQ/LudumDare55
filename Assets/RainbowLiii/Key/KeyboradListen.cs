using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    [HideInInspector] public float tempTime;
    public GameObject buff;
    private BuffSystem buffSystem;
    public Sprite[] sprites;
    public Sprite[] backs;
    void Start()
    {
        showKeys = new List<ShowKey>();
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        GL = GameListenner.GetComponent<GameListenner>();
        buffSystem = buff.GetComponent<BuffSystem>();
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
        if(Input.GetKeyDown(KeyCode.L)) buffSystem.Summoning();


        if (MaxKey <= 7 && !pc.enableCall)
        {
            TimeListenner();
        }
        if (!isSequenceStarted && Input.anyKeyDown && pc.enableCall && pc.getNum >= GL.MaxKey)
        {
            isSequenceStarted = true;

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

    }
    private void FixedUpdate()
    {
        if (pc.enableCall)
            Timelimit();
    }

    public AudioSource correctSE;
    public AudioSource inCorrectSE;
    public AudioSource apSE;

    void KeyTag()
    {
        //Debug.Log(1);
        if (Input.GetKeyDown(KeyCode.W))
        {
            inputSequence.Add(1);
            correctSequence.Add(showKeys[i].num);
            showKeys[i].keyW.GetComponent<Image>().sprite = sprites[0];
            if (inputSequence[i] == correctSequence[i])
            {
                correctSE.Play();
                showKeys[i].keyW.GetComponent<Image>().color = Color.green;
                // Debug.Log("����1�����£����ӵ������С�");
                // Debug.Log(correctSequence.Count);
                i++;
            }
            else
            {
                inCorrectSE.Play();
                Debug.Log("���û����ȷ�ذ�˳�򰴼���");
                showKeys[i].keyW.GetComponent<Image>().color = Color.red;
                BadBuff();
                ResetSequence();
            }

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            inputSequence.Add(2);
            correctSequence.Add(showKeys[i].num);
            showKeys[i].keyA.GetComponent<Image>().sprite = sprites[1];
            if (inputSequence[i] == correctSequence[i])
            {
                correctSE.Play();
                showKeys[i].keyA.GetComponent<Image>().color = Color.green;
                // Debug.Log("����2�����£����ӵ������С�");
                // Debug.Log(correctSequence.Count);
                i++;
            }
            else
            {
                inCorrectSE.Play();
                Debug.Log("���û����ȷ�ذ�˳�򰴼���");
                showKeys[i].keyA.GetComponent<Image>().color = Color.red;
                BadBuff();
                ResetSequence();
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            inputSequence.Add(3);
            correctSequence.Add(showKeys[i].num);
            showKeys[i].keyS.GetComponent<Image>().sprite = sprites[2];
            if (inputSequence[i] == correctSequence[i])
            {
                correctSE.Play();
                showKeys[i].keyS.GetComponent<Image>().color = Color.green;
                // Debug.Log("����3�����£����ӵ������С�");
                // Debug.Log(correctSequence.Count);
                i++;
            }
            else
            {
                inCorrectSE.Play();
                Debug.Log("���û����ȷ�ذ�˳�򰴼���");
                showKeys[i].keyS.GetComponent<Image>().color = Color.red;
                BadBuff();
                ResetSequence();
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            inputSequence.Add(4);
            correctSequence.Add(showKeys[i].num);
            showKeys[i].keyD.GetComponent<Image>().sprite = sprites[3];
            if (inputSequence[i] == correctSequence[i])
            {
                correctSE.Play();
                showKeys[i].keyD.GetComponent<Image>().color = Color.green;
                // Debug.Log("����4�����£����ӵ������С�");
                // Debug.Log(correctSequence.Count);
                i++;
            }
            else
            {
                inCorrectSE.Play();
                Debug.Log("���û����ȷ�ذ�˳�򰴼���");
                showKeys[i].keyD.GetComponent<Image>().color = Color.red;
                BadBuff();
                ResetSequence();
            }
        }
    }

    void Check()
    {

        if (inputSequence.SequenceEqual(correctSequence))
        {
            apSE.Play();
            //Debug.Log("�����ȷ�ذ�˳�򰴼��ˣ�");
            BulletChatController.instance.AddBulletChat("iRui", "You sing to my heart!!");
            ShowBuff();
            ResetSequence();
        }
        else
        {
            Debug.Log("���û����ȷ�ذ�˳�򰴼���");
            BadBuff();
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
        pc.getNum = 0;
        foreach(var showkey in showKeys)
        {
            showkey.keyW.GetComponent<Image>().sprite = backs[0];
            showkey.keyA.GetComponent<Image>().sprite = backs[1];
            showkey.keyS.GetComponent<Image>().sprite = backs[2];
            showkey.keyD.GetComponent<Image>().sprite = backs[3];
            showkey.keyW.GetComponent<Image>().color = Color.white;
            showkey.keyA.GetComponent<Image>().color = Color.white;
            showkey.keyS.GetComponent<Image>().color = Color.white;
            showkey.keyD.GetComponent<Image>().color = Color.white;
        }
        //gameObject.SetActive(false);
        i = 0;
    }
    void Timelimit()
    {
        if (escapTime >= MaxTime)
        {
            ResetSequence();
            BadBuff();
            //Debug.Log("YES");
        }
        else
        {
            escapTime += Time.deltaTime;

        }
    }
    void TimeListenner()
    {
        if (GL.GameTime >= tempTime)
        {
            keys[MaxKey].SetActive(true);
            MaxKey += 1;
            tempTime += BtwTime;
        }
    }
    void ShowBuff()
    {
        int randomnum = Random.Range(0, 6);
        //int randomnum = 5;
        if (randomnum == 0)
        {
            buffSystem.AddDamage();
        }
        else if (randomnum == 1)
        {
            buffSystem.AddSpeed();
        }
        else if (randomnum == 2)
        {
            buffSystem.AddHealth();
        }
        else if (randomnum == 3)
        {
            buffSystem.Invincible();
        }
        else if (randomnum == 4)
        {
            buffSystem.ReduceTime();
        }
        else if (randomnum == 5)
        {
            buffSystem.Summoning();
        }
    }
    void BadBuff()
    {
        BulletChatController.instance.AddBulletChat("Cosmo", "I think it's out of tune...");
        int rannum = Random.Range(0, 5);
        //int rannum = 3;
        if (rannum == 0)
        {
            buffSystem.ReduceSpeed();
        }
        else if (rannum == 1)
        {
            buffSystem.AddTime();
        }
        else if (rannum == 2)
        {
            buffSystem.ReduceDamage();
        }
        else if (rannum == 3)
        {
            buffSystem.ReduceTeam();
        }
        else if (rannum == 4)
        {
            buffSystem.CallEnemy();
        }
    }
}
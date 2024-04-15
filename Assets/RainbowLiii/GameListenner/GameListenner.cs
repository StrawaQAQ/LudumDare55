using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine;

public class GameListenner : MonoBehaviour
{
    // public GameObject KL;
    //private KeyboradListen kl;
    [Header("游戏进行时间")]
    public float GameTime;
    public int MaxKey;
    public float btwtime;
    public float temptime;
    public float barrageTime;
    private float btwTime;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        //kl = KL.GetComponent<KeyboradListen>();
        BulletChatController.instance.AddBulletChat("Manager", "The live stream is on!");
        btwTime = 0f;
        MaxKey = 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameTime += Time.deltaTime;
        if(GameTime >= temptime)
        {
            MaxKey++;
            if(MaxKey > 8)
            {
                MaxKey = 8;
            }
            temptime += btwtime;
        }
        if(btwTime >= barrageTime)
        {
            barrage();
            btwTime = 0f;
        }
        else
        {
            btwTime += Time.deltaTime;
        }
    }
    void barrage()
    {
        int randomNum = Random.Range(0, 5);
        if(randomNum == 0)
        {
            BulletChatController.instance.AddBulletChat("Hang", "Hello, anchor");
        }
        else if(randomNum == 1)
        {
            BulletChatController.instance.AddBulletChat("Hong", "I really expect");
        }
        else if(randomNum == 2)
        {
            BulletChatController.instance.AddBulletChat("Rainbow", "When is the next live broadcast？");
        }
        else if(randomNum == 3)
        {
            BulletChatController.instance.AddBulletChat("Xixi", "It's not bad.");
        }
        else if(randomNum == 4)
        {
            BulletChatController.instance.AddBulletChat("SugerMan", "How long will the anchor be on today？");
        }
    }
}

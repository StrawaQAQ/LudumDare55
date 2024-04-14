using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        //kl = KL.GetComponent<KeyboradListen>();
        
        MaxKey = 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameTime += Time.deltaTime;
        if(GameTime >= temptime)
        {
            MaxKey++;
            temptime += btwtime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowKey : MonoBehaviour
{
    public GameObject keyW;
    public GameObject keyA;
    public GameObject keyS;
    public GameObject keyD;
    private GameObject keyShow;
    public PlayerControl pc;
    private float keyNum;
    public float num;
    // Start is called before the first frame update
   public void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (pc.enableCall)
        {
            keyShow.SetActive(true);
            num = keyNum;
        }
        else
        {
            RandomKey();
            keyShow.SetActive(false);
        }
        
    }
    public void RandomKey()
    {
        float key = Random.Range(1, 5);
        keyNum = key;
        if(key == 1)
        {
            keyShow = keyW;
        }
        else if(key == 2)
        {
            keyShow = keyA;
        }
        else if(key == 3)
        {
            keyShow = keyS;
        }
        else if(key == 4)
        {
            keyShow = keyD;
        }
    }
}

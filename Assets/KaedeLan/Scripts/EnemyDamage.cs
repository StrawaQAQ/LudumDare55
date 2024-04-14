using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public Collider2D coll;
    public float damage;
    public bool hitPlayer = false;
    public int k = 0;
    public int pow;

    void Awake()
    {
        k = 0;
        hitPlayer = false;
    }

    void OnEnable()
    {
        k = 0;
    }

    // void OnTriggerStay2D(Collider2D other)
    // {
    //     if(k == 0)
    //     {
    //         Debug.Log("哦哈哟");
    //         HongPlayer a = other.GetComponent<HongPlayer>();
    //         if (a != null && other.tag == "Player")
    //         {
    //             Debug.Log("学妹");
    //             a.TakeDamage(pow);
    //             k ++;
    //         }
    //     }

    // }

    void OnTriggerStay2D(Collider2D other)
    {
        if(k == 0)
        {
            Debug.Log("哦哈哟");
            PlayerControl a = other.GetComponent<PlayerControl>();
            if (a != null && other.tag == "Player")
            {
                Debug.Log("学妹");
                a.TakeDamage(pow);
                k ++;
            }
        }

    }
}

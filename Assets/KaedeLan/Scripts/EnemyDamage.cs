using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public bool magicButton = false;
    public bool debugMode = false;
    public GameObject audioWave;
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
        Debug.Log(other.tag);
        if(k == 0)
        {
            if(debugMode)
            {
                if(magicButton) {
                    Instantiate(audioWave, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }
            if(other.TryGetComponent(out getDamage getDamage))
            {
                if (other.CompareTag("Player") || other.CompareTag("Summoning"))
                {
                    k++;
                    getDamage.TakeDamage(pow);
                }
                if (magicButton)
                {
                    Instantiate(audioWave, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }

            //PlayerControl a = other.GetComponent<PlayerControl>();
            //if (a != null && other.tag == "Player")
            //{
            //    a.TakeDamage(pow);
            //    k ++;
            //    if(magicButton) {
            //        Instantiate(audioWave, transform.position, transform.rotation);
            //        Destroy(gameObject);
            //    }
            //}else{
            //    FollowController fc = other.GetComponent<FollowController>();
            //    if (fc != null && other.tag == "Summon")
            //    {
            //        PlayerControl b = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
            //        //b.attackRange_ = new Vector2(b.attackRange_.x-0.5f, b.attackRange_.y-0.25f);
            //        fc.RemoveOneFollower();
            //        k ++;
            //        if(magicButton) {
            //            Instantiate(audioWave, transform.position, transform.rotation);
            //            Destroy(gameObject);
            //        }
            //    }
            //}
        }

    }
}

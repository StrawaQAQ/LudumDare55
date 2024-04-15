using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCont : MonoBehaviour
{
    public Sprite[] note;
    public SpriteRenderer sr;
    private Animator anim;

    void Awake()
    {
        sr.sprite = note[(int)Random.Range(0,3)];
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControl a = other.GetComponent<PlayerControl>();
        if (a != null && other.tag == "Player")
        {
            a.getNum ++;
            a.CollectPlay();
            anim.Play("Dead");
        }
    }

    void Suiside()
    {
        Destroy(gameObject);
    }
}

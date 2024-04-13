using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemy : MonoBehaviour
{
    protected Collider2D coll;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected int HP;
    [Header("最大生命值")]
    public int _HP;
    [Header("攻击力")]
    public int pow;
    [Header("移速")]
    public float speed;
    public string condition = "";

    protected void Awake()
    {
        HP = _HP;
        condition = "idle";
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    protected void FixedUpdate()
    {
        if(HP <= 0)
        {
            condition = "death";
            anim.Play("Dead");
        }
    }
}

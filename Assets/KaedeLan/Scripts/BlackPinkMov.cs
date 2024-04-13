using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPinkMov : AllEnemy
{
    public EnemyVision enemyVision;
    [Header("在距离玩家多少距离的地方展开攻击")]
    public float stopRange;
    [Header("攻击碰撞体")]
    public GameObject attackP;
    private Transform player;

    void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player").transform;
    }

    void ChasePlayer()
    {
        player = GameObject.FindWithTag("Player").transform;
        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, player.position, step);

        if(Mathf.Abs(transform.position.x - player.position.x) <= stopRange && Mathf.Abs(transform.position.y - player.position.y) <= stopRange)
        {
            condition = "hit";
            anim.Play("Hit");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        base.FixedUpdate();
        if(condition != "death")
        {
            if(enemyVision.inRange && (condition=="idle"))
            {
                condition = "dash";
                anim.Play("Chase");
            }else if(!enemyVision.inRange && (condition=="dash"))
            {
                condition = "idle";
                anim.Play("Idle");
            }else{
                if(condition == "dash") ChasePlayer();
            }
        }
    }

    void toIdle()
    {
        anim.Play("Idle");
        condition = "idle";
    }

    void Hit()
    {
        attackP.SetActive(true);
    }

    void StopHit()
    {
        attackP.SetActive(false);
    }
}

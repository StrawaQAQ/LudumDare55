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
    public int turningRight = 1;
    private SpriteRenderer sr;
    public float tempRec;
    public AudioSource walkSE;
    public AudioSource hitSE;
    private Transform target;
    private bool hitSth = false;

    void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
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

    void playWalkSE()
    {
        walkSE.Play();
    }

    void stopSE()
    {
        walkSE.Stop();
    }

    void playHitSE()
    {
        hitSE.pitch = Random.Range(0.7f, 1.2f);
        hitSE.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        base.FixedUpdate();
        if(condition != "death")
        {
            if(!hitSth) target = player;
            if(enemyVision.inRange && (condition=="idle"))
            {
                condition = "dash";
                anim.Play("Chase");
            }else if(!enemyVision.inRange && (condition=="dash"))
            {
                walkSE.Pause();
                condition = "idle";
                anim.Play("Idle");
            }else{
                if(condition == "dash") ChasePlayer();
            }
        }
        if(condition != "idle")
        {
            if(transform.position.x - player.position.x > 0.3f && turningRight == 1)  turnAround();
            else if(player.position.x - transform.position.x > 0.3f && turningRight == -1) turnAround();
        }
        
    }

    void turnAround()
    {
        // Debug.Log("你好");
        if(turningRight == 1)
        {
            turningRight = -1;
            sr.flipX = true;
        }else{
            turningRight = 1;
            sr.flipX = false;
        }
        // transform.localScale = new Vector3(turningRight, 1, 0);
        attackP.transform.localScale = new Vector3(turningRight, 1, 0);
        
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

    void OnColliderEnter2D(Collider2D other)
    {
        //  玩家在人物上方
        if(player.transform.position.y > transform.position.y)
        {
            
        }
    }
}

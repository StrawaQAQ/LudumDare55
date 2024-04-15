using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarHeroMov : AllEnemy
{
    public EnemyVision enemyVision;
    [Header("子弹预制体")]
    public GameObject[] audioWave;
    [Header("在距离玩家多少距离的地方展开攻击")]
    public float stopRange;
    [Header("来到玩家多近的距离")]
    public Vector2 hitRange = new Vector2 (4f,4f);
    private Transform player;
    private int a = 1;
    public int turningRight = 1;
    private SpriteRenderer sr;
    public AudioSource asa;
    private Vector2 destination;

    void Awake()
    {
        base.Awake();
        a = 1;
        player = GameObject.FindWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
    }

    void ChasePlayer()
    {
        player = GameObject.FindWithTag("Player").transform;
        RollDest();
    }

    void Approach()
    {
        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, destination, step);

        if(Mathf.Abs(transform.position.x - destination.x) <= stopRange && Mathf.Abs(transform.position.y - destination.y) <= stopRange)
        {
            condition = "hit";
            anim.Play("Hit");
        }
    }

    void RollDest()
    {
        destination = new Vector2(player.position.x+Random.Range(0.2f,hitRange.x),player.position.y+Random.Range(0.2f,hitRange.y));
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
                ChasePlayer();
                anim.Play("Chase");
            }else if(!enemyVision.inRange && (condition!="idle"))
            {
                condition = "idle";
                anim.Play("Idle");
            }else{
                if(condition == "dash") Approach();
                // else if(condition == "idle")
            }
        }
        if(condition != "idle")
        {
            if(transform.position.x - player.position.x > 0.1f && turningRight == 1)  turnAround();
            else if(player.position.x - transform.position.x > 0.1f && turningRight == -1) turnAround();
        }else{
            if(rb.velocity.x > 0f && turningRight == -1)  turnAround();
            else if(rb.velocity.x < 0f && turningRight == 1)  turnAround();
        }
    }

    void AsPlay()
    {
        asa.pitch = Random.Range(0.7f, 1.2f);
        asa.Play();
    }

    void AsStop()
    {
        asa.Pause();
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
        // DamageRange.transform.localScale = new Vector3(turningRight, 1, 0);
        
    }

    void addVelocity()
    {
        rb.velocity = new Vector2(Random.Range(0f,2f) * a, Random.Range(0f,2f) * a);
        a = a * (-1);
    }

    void clearVelocity()
    {
        rb.velocity = new Vector2(0f, 0f);
    }

    void toIdle()
    {
        if(!enemyVision.inRange)
        {
            anim.Play("Idle");
            condition = "idle";
        }else{
            condition = "dash";
            ChasePlayer();
            anim.Play("Chase");
        }
    }

    void Hit()
    {
        for(int i=0; i<8; i++)
        {
            Instantiate(audioWave[i], transform.position, transform.rotation);
        }
    }
}

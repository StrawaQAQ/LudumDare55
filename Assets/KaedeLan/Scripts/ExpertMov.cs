using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpertMov : AllEnemy
{
    public EnemyVision enemyVision;
    [Header("陨石预制体")]
    public GameObject audioWave;
    [Header("在距离玩家多少距离的地方展开攻击")]
    public float stopRange;
    [Header("攻击范围")]
    public Vector2 hitRange = new Vector2 (4f,4f);
    private Transform player;
    private int a = 1;
    public int turningRight = 1;
    private SpriteRenderer sr;
    public AudioSource asa;

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
                // else if(condition == "idle")
            }
        }
        if(condition != "idle")
        {
            if(transform.position.x - player.position.x > 0.3f && turningRight == 1)  turnAround();
            else if(player.position.x - transform.position.x > 0.3f && turningRight == -1) turnAround();
        }
    }

    void AsPlay()
    {
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
        }
    }

    void Hit()
    {
        float k = Random.Range(0,1f);
        if(k < 0.5f)    Instantiate(audioWave, new Vector3(transform.position.x+Random.Range(0.2f,hitRange.x),transform.position.y+Random.Range(0.2f,hitRange.y),transform.position.z), transform.rotation);
        else            Instantiate(audioWave, new Vector3(transform.position.x-Random.Range(0.2f,hitRange.x),transform.position.y-Random.Range(0.2f,hitRange.y),transform.position.z), transform.rotation);
    }
}

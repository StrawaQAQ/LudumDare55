using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuffSystem : MonoBehaviour
{
    [Header("增益")]
    public int addDamage;
    public float addSpeed;
    public float reduceTime;
    public int addhealth;
    public float Invincibletime;
    public bool enableIn;
    private float tempTime;
    private PlayerControl pc;
    public GameObject summoning;
    [Header("惩罚")]
    public float reduceSpeed;
    public float addTime;
    public int reduceDamage;
    public GameObject[] enemys;
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        enableIn = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enableIn)
        {
            if (tempTime >= Invincibletime)
            {
                pc.cap.enabled = true;
                enableIn = false;
            }
            else
            {
                pc.cap.enabled = false;
                tempTime += Time.deltaTime;
            }
        }

    }
    //增益
    public void AddDamage()
    {
        pc.damage += addDamage;
        BulletChatController.instance.AddBulletChat("Manager", "The anchor receives the gift [Golden microphone]!");
    }
    public void AddSpeed()
    {
        pc.speed += addSpeed;
        BulletChatController.instance.AddBulletChat("Manager", "The anchor receives the gift [Rocket]!");
    }
    public void ReduceTime()
    {
        pc.MaxTime -= reduceTime;
        BulletChatController.instance.AddBulletChat("Manager", "The anchor receives the gift [Throat Lozenge]!");
        if (pc.MaxTime <= 0)
        {
            pc.MaxTime = 0f;
        }
    }
    public void AddHealth()
    {
        pc.health += addhealth;
        BulletChatController.instance.AddBulletChat("Manager", "The anchor receives the gift [Heart]!");
    }
    public void Invincible()
    {
        enableIn = true;
        BulletChatController.instance.AddBulletChat("Manager", "The anchor receives the gift [Crown]!");
    }
    public void Summoning()
    {
        BulletChatController.instance.AddBulletChat("Manager", "One  [Big Fan] enter the studio!");
        int randomnum = Random.Range(0, 5);
        Vector2 point = new Vector2(pc.transform.position.x + randomnum, pc.transform.position.y + randomnum).normalized * 2;
        pc.attackRange = new Vector2(pc.attackRange.x + 2f, pc.attackRange.y + 1f);
        Instantiate(summoning, point, Quaternion.identity);
    }
    //惩罚
    public void ReduceSpeed()
    {
        pc.speed -= reduceSpeed;
        BulletChatController.instance.AddBulletChat("Manager", "The anchor is a little tired");
        if (pc.speed <= 10)
        {
            pc.speed = 10f;
        }
    }
    public void AddTime()
    {
        pc.MaxTime += addTime;
        BulletChatController.instance.AddBulletChat("Manager", "The anchor seems thirsty");
    }
    public void ReduceDamage()
    {
        pc.damage -= reduceDamage;
        BulletChatController.instance.AddBulletChat("Manager", "One [Fan] leave the studio");
        if (pc.damage <= 1)
        {
            pc.damage = 1;
        }
    }
    public void ReduceTeam()
    {
        BulletChatController.instance.AddBulletChat("Manager", "One [Big Fan] leave the studio");
        GameObject[] destroyableObjects = GameObject.FindGameObjectsWithTag("Summoning");

        if (destroyableObjects.Length > 0)
        {
            // 随机选择一个游戏对象进行销毁  
            int randomIndex = Random.Range(0, destroyableObjects.Length);
            pc.attackRange = new Vector2(pc.attackRange.x - 2f, pc.attackRange.y - 1f);
            if (pc.attackRange.x <= 0 && pc.attackRange.y <= 0f)
            {
                pc.attackRange = new Vector2(4f, 2f);
            }
            Destroy(destroyableObjects[randomIndex]);
        }
        else
        {
            CallEnemy();
        }
    }
    public void CallEnemy()
    {
        BulletChatController.instance.AddBulletChat("Manager", "The anchor is playing PK with other anchors");
        for (int i = 0; i < enemys.Length; i++)
        {
            Instantiate(enemys[i], new Vector2(pc.transform.position.x + Random.Range(3, 5), pc.transform.position.y + Random.Range(3, 5)), Quaternion.identity);
        }
    }
}

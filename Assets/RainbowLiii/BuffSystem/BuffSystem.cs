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
    [Header("特效")]
    public GameObject[] VFXs;
    //FollowController followController = new FollowController();
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
        Instantiate(VFXs[0], pc.transform.position, Quaternion.identity);
    }
    public void AddSpeed()
    {
        pc.speed += addSpeed;
        BulletChatController.instance.AddBulletChat("Manager", "The anchor receives the gift [Rocket]!");
        Instantiate(VFXs[1], pc.transform.position, Quaternion.identity);
    }
    public void ReduceTime()
    {
        pc.MaxTime -= reduceTime;
        BulletChatController.instance.AddBulletChat("Manager", "The anchor receives the gift [Throat Lozenge]!");
        Instantiate(VFXs[2], pc.transform.position, Quaternion.identity);
        if (pc.MaxTime <= 0)
        {
            pc.MaxTime = 0f;
        }
    }
    public void AddHealth()
    {
        pc.health += addhealth;
        BulletChatController.instance.AddBulletChat("Manager", "The anchor receives the gift [Heart]!");
        Instantiate(VFXs[3], pc.transform.position, Quaternion.identity);
    }
    public void Invincible()
    {
        enableIn = true;
        BulletChatController.instance.AddBulletChat("Manager", "The anchor receives the gift [Crown]!");
    }
    public void Summoning()
    {
        BulletChatController.instance.AddBulletChat("Manager", "One  [Big Fan] enter the studio!");
        pc.attackRange = new Vector2(pc.attackRange.x + 0.5f, pc.attackRange.y + 0.25f);
        Instantiate(VFXs[4], pc.transform.position, Quaternion.identity);
        FollowController.instance.AddFollower();
    }
    //惩罚
    public void ReduceSpeed()
    {
        pc.speed -= reduceSpeed;
        BulletChatController.instance.AddBulletChat("Manager", "The anchor is a little tired");
        Instantiate(VFXs[5], pc.transform.position, Quaternion.identity);
        if (pc.speed <= 10)
        {
            pc.speed = 10f;
        }
    }
    public void AddTime()
    {
        pc.MaxTime += addTime;
        BulletChatController.instance.AddBulletChat("Manager", "The anchor seems thirsty");
        Instantiate(VFXs[6], pc.transform.position, Quaternion.identity);
    }
    public void ReduceDamage()
    {
        pc.damage -= reduceDamage;
        BulletChatController.instance.AddBulletChat("Manager", "One [Fan] leave the studio");
        Instantiate(VFXs[7], pc.transform.position, Quaternion.identity);
        if (pc.damage <= 1)
        {
            pc.damage = 1;
        }
    }
    public void ReduceTeam()
    {
        BulletChatController.instance.AddBulletChat("Manager", "One [Big Fan] leave the studio");
        Instantiate(VFXs[8], new Vector2(pc.transform.position.x, pc.transform.position.y - 1.29f), Quaternion.identity);
        GameObject[] destroyableObjects = GameObject.FindGameObjectsWithTag("Summoning");

        if (destroyableObjects.Length > 0)
        {
            // 随机选择一个游戏对象进行销毁  
            int randomIndex = Random.Range(0, destroyableObjects.Length);
            pc.attackRange = new Vector2(pc.attackRange.x - 0.5f, pc.attackRange.y - 0.25f);
            if (pc.attackRange.x <= 0 && pc.attackRange.y <= 0f)
            {
                pc.attackRange = new Vector2(0.5f, 0.25f);
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
        Instantiate(VFXs[9], new Vector2(pc.transform.position.x,pc.transform.position.y - 1.29f), Quaternion.identity);
        for (int i = 0; i < enemys.Length; i++)
        {
            Instantiate(enemys[i], new Vector2(pc.transform.position.x + Random.Range(3, 5), pc.transform.position.y + Random.Range(3, 5)), Quaternion.identity);
        }
    }
}

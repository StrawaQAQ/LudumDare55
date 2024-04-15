using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HongEnemyRoller : MonoBehaviour
{
    [Header("生成敌人间隔时间")]
    public float _CD;
    private float CD;
    private float gameStartCD = 3f;

    [Header("生成敌人的表单长度")]
    public int length;
    [Header("敌人列表（如果想要提高某个敌人的出场概率，就多加几个）")]
    public GameObject[] enemy;
    public GameObject VFX;

    void FixedUpdate()
    {
        if(gameStartCD <= 0)
        {
            if(CD <= 0)
            {
                Debug.Log("aaaaa");
                int ran = Random.Range(0,length);
                Instantiate(enemy[ran], transform.position, transform.rotation);
                switch(ran)
                {
                    case 0:{BulletChatController.instance.AddBulletChat("<color=#ff0000>MyGiegieNO1</color>", "<color=#ff0000>You're not a good singer!</color>");}break;
                    case 1:{BulletChatController.instance.AddBulletChat("<color=#ff0000>Expert</color>", "<color=#ff0000>You're about to break your voice!</color>");}break;
                    case 2:{BulletChatController.instance.AddBulletChat("<color=#ff0000>GuitarHero</color>", "<color=#ff0000>Can you keep up with my music?</color>");}break;
                    case 3:{BulletChatController.instance.AddBulletChat("<color=#ff0000>MySongNO1</color>", "<color=#ff0000>I want to sing with you...</color>");}break;
                }
                Instantiate(VFX, new Vector2(transform.position.x, transform.position.y-1.29f), transform.rotation);
                CD = _CD;
            }else{
                CD -= Time.fixedDeltaTime;
            }
        }else{
            gameStartCD -= Time.fixedDeltaTime;
        }
        
    }
}

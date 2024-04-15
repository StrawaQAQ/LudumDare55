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
                int ran = Random.Range(0,length);
                Instantiate(enemy[ran], transform.position, transform.rotation);
                switch(ran)
                {
                    case 0:{BulletChatController.instance.AddBulletChat("MyGiegieNO1", "You're not a good singer！");}break;
                    case 1:{BulletChatController.instance.AddBulletChat("Expert", "You're about to break your voice！");}break;
                    case 2:{BulletChatController.instance.AddBulletChat("GuitarHero", "Can you keep up with my music？");}break;
                    case 3:{BulletChatController.instance.AddBulletChat("MySongNO1", "I want to sing with you...");}break;
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

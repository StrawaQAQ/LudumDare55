using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HongEnemyRoller : MonoBehaviour
{
    [Header("生成敌人间隔时间")]
    public float _CD;
    private float CD;

    [Header("生成敌人的表单长度")]
    public int length;
    [Header("敌人列表（如果想要提高某个敌人的出场概率，就多加几个）")]
    public GameObject[] enemy;

    void FixedUpdate()
    {
        if(CD <= 0)
        {
            Instantiate(enemy[(int)Random.Range(0,length)], transform.position, transform.rotation);
            CD = _CD;
        }else{
            CD -= Time.fixedDeltaTime;
        }
    }
}

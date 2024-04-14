using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemy : MonoBehaviour, getDamage
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
    [Header("攻击范围")]
    public GameObject enemyDamage;
    public bool hasDamage = false;

    protected void Awake()
    {
        HP = _HP;
        condition = "idle";
        if(hasDamage)   enemyDamage.GetComponent<EnemyDamage>().pow = this.pow;
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

    public string getCurrentClipInfo() // 获取当前执行的动画
    {
        AnimatorClipInfo[] m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        return m_CurrentClipInfo[0].clip.name;
    }

    public void enableDf()
    {
        enemyDamage.SetActive(true);
    }

    public void disableDf()
    {
        enemyDamage.SetActive(false);
    }

    #region 受击接口
    public void TakeDamage(int damage)
    {
        HP -= damage;
    }
    #endregion

    void Destroy()
    {
        Destroy(gameObject);
    }
}

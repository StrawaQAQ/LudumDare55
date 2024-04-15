using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommonDamage : MonoBehaviour, getDamage
{
    public BlackPinkMov blackPinkMov;
    public ExpertMov expertMov;
    public GuitarHeroMov guitarHeroMov;
    public WildSingerMov WildSingerMov;
    public int index;

    #region 受击接口
    public void TakeDamage(int damage)
    {
        switch(index)
        {
            case 0:{blackPinkMov.HP -= damage;blackPinkMov.anim.Play("Hurt");}break;
            case 1:{expertMov.HP -= damage;expertMov.anim.Play("Hurt");}break;
            case 2:{guitarHeroMov.HP -= damage;guitarHeroMov.anim.Play("Hurt");}break;
            case 3:{WildSingerMov.HP -= damage;WildSingerMov.anim.Play("Hurt");}break;
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HongPlayer : MonoBehaviour,getDamage
{
    Rigidbody2D rb;
    public int HP = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    } 
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 1f);
        }else if (Input.GetKey("a"))
        {
            rb.velocity = new Vector2(-1f, rb.velocity.y);
        }else if (Input.GetKey("s"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -1f);
        }
        else if(Input.GetKey("d")){
            rb.velocity = new Vector2(1f, rb.velocity.y);
        }else{
            rb.velocity = new Vector2(0,0);
        }
    }

    #region 受击接口
    public void TakeDamage(int damage)
    {
        HP -= damage;
    }
    #endregion
}

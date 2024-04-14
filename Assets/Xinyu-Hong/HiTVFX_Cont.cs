using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiTVFX_Cont : MonoBehaviour
{
   public float speed = 0f;
   private float CD = 3f;
   private Rigidbody2D rb;

   void Suiside()
   {
        Destroy(gameObject);
   }

   void Awake()
   {
      if(speed != 0f)
      {
         rb = GetComponent<Rigidbody2D>();
      }
   }

   void FixedUpdate()
   {
      if(speed != 0f)
      {
         CD -= Time.fixedDeltaTime;
         rb.velocity = new Vector2(speed*CD*2, 0f);

      }
   }
}

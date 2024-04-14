using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiTVFX_Cont : MonoBehaviour
{
   public Vector2 speed = new Vector2(0f,0f);
   public bool magicButton;
   private float CD = 3f;
   private Rigidbody2D rb;

   void Suiside()
   {
        Destroy(gameObject);
   }

   void Awake()
   {
      if(speed.x != 0f || speed.y != 0f)
      {
         rb = GetComponent<Rigidbody2D>();
      }
   }

   void FixedUpdate()
   {
      if(magicButton)
      {
         CD -= Time.fixedDeltaTime;
         rb.velocity = new Vector2(speed.x*CD*2, speed.y*CD*2);
      }
   }
}

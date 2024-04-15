using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Chen : MonoBehaviour
{
    public float speed;
    private Rigidbody2D Rigidbody2D;

    private void Awake()
    {

        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) Rigidbody2D.velocity = new Vector2(-speed, Rigidbody2D.velocity.y);
        else if (Input.GetKey(KeyCode.D)) Rigidbody2D.velocity = new Vector2(speed, Rigidbody2D.velocity.y);
        else Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
        if (Input.GetKey(KeyCode.W)) Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, speed);
        else if (Input.GetKey(KeyCode.S)) Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, -speed);
        else Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0);
    }
}

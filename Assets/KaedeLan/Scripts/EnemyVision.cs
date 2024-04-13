using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public bool inRange = false;
    private Collider2D coll;

    void Awake()
    {
        inRange = false;
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            inRange = false;
        }
    }
}

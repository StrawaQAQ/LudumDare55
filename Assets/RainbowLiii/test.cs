using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage()
    {
        GameObject.FindWithTag("Player").GetComponent<getDamage>().TakeDamage(damage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Microphone;
    private MicrophoneInput MI;
    public CircleCollider2D circle;
    public float MaxRange;
    public float MaxTime;
    public float esctime;
    // Start is called before the first frame update
    void Start()
    {
        MI = Microphone.GetComponent<MicrophoneInput>();
        circle = GetComponent<CircleCollider2D>();
        esctime = MaxTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(esctime >= MaxTime)
        {
            circle.radius = MI.realVolume / MaxRange;
            circle.enabled = true;
            esctime = 0f;
        }
        else
        {
            esctime += Time.deltaTime;
            circle.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}

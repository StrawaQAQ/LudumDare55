using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Microphone;
    private MicrophoneInput MI;
    public CapsuleCollider2D cap;
    private PlayerControl pc;
    public float MaxRange;
    public float MaxTime;
    public float esctime;
    public int damage;
    public Vector2 Range;
    // Start is called before the first frame update
    void Start()
    {
        MI = Microphone.GetComponent<MicrophoneInput>();
        cap = GetComponent<CapsuleCollider2D>();
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        esctime = MaxTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MaxTime = pc.MaxTime;
        damage = pc.damage;
        Range = pc.attackRange;
        if(esctime >= MaxTime && MI.realVolume > 1f)
        {
            cap.size = Range;
            cap.enabled = true;
            esctime = 0f;
        }
        else
        {
            esctime += Time.deltaTime;
            cap.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}

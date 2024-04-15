using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Microphone;
    private MicrophoneInput MI;
    public CapsuleCollider2D cap;
    private PlayerControl pc;
    private ParticleSystem ps;
    //public float MaxRange;
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
        ps = GetComponent<ParticleSystem>();
        esctime = MaxTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MaxTime = pc.MaxTime;
        damage = pc.damage;
        Range = pc.attackRange;
        if(esctime >= MaxTime && (MI.realVolume > 1f || Input.GetKey(KeyCode.E)))
        {
            transform.localScale = Range;
            cap.enabled = true;
            ps.Play();
            esctime = 0f;
        }
        else
        {
            esctime += Time.deltaTime;
            ps.Stop(true);
            cap.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyCommonDamage a = other.GetComponent<EnemyCommonDamage>();
        Debug.Log("O");
        // collision.GetComponent<EnemyCommonDamage>().TakeDamage(damage);
        if (a != null && other.tag == "EnemyB")
        {
            Debug.Log("啊");
            a.TakeDamage(damage);
        }
    }
}

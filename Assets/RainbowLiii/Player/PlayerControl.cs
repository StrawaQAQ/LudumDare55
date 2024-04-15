using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour,getDamage
{
    [HideInInspector]public Animator anim;
    [Header("����ֵ")]
    public int health;
    private Rigidbody2D rb;
    Vector2 moveInput;
    public PlayerInput input;
    [Header("�ƶ��ٶ�")]
    public float speed;
    public bool enableMove;
    [Header("ʰȡ��item����")]
    public int getNum;
    public bool enableCall;
    [Header("���̼���")]
    public GameObject kl;
    private KeyboradListen Kl;
    public CapsuleCollider2D cap;
    public int damage;
    public float MaxTime;
    public Vector2 attackRange;
    public GameObject BF;
    private BuffSystem bf;
    public GameObject VFX;
    void Start()
    {
        input = new PlayerInput();
        input.Enable();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Kl = kl.GetComponent<KeyboradListen>();
        cap = GetComponent<CapsuleCollider2D>();
        bf = BF.GetComponent<BuffSystem>();
        enableMove = true;
        enableCall = false;
        attackRange = new Vector2(0.5f, 0.25f);
    }
    void FixedUpdate()
    {
        OnMove();
        
    }
    private void Update()
    {
        ChangeInput();
        if (enableCall)
        {
            anim.SetBool("sing", true);
            Sing();
        }
        else
        {
            anim.SetBool("sing", false);
        }
        if (bf.enableIn)
        {
            VFX.SetActive(true);
        }
        else
        {
            VFX.SetActive(false);
        }
    }
    private void OnMove()
    {
        if (enableMove)
        {
            //anim.Play("Idle");
            moveInput = input.Player.Move.ReadValue<Vector2>();
            rb.velocity = new Vector2(moveInput.x * speed * Time.deltaTime, moveInput.y * speed * Time.deltaTime);
            if(moveInput.x < 0f)
            {
                rb.transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                rb.transform.localScale = new Vector2(1, 1);
            }
            anim.SetFloat("Horizontal", moveInput.x);
            anim.SetFloat("Vertical", moveInput.y);
            anim.SetFloat("Magnitude", moveInput.magnitude);
        }
        else
        {
            moveInput = Vector2.zero;
            rb.velocity = Vector2.zero;
            anim.SetFloat("Magnitude", 0);
            
        }
    }
    
   private void ChangeInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (enableMove && getNum >= Kl.MaxKey)
            {
                enableMove = false;
                enableCall = true;
                kl.SetActive(true);
            }
            else if (!enableMove)
            {
                enableMove = true;
                enableCall = false;
                kl.SetActive(false);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Sing()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetFloat("Horizontal", 1);
            anim.SetFloat("Vertical", 0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetFloat("Horizontal", -1);
            anim.SetFloat("Vertical", 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetFloat("Vertical", 1);
            anim.SetFloat("Horizontal", 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetFloat("Vertical", -1);
            anim.SetFloat("Horizontal", 0);
        }
    }

    public AudioSource collect;

    public void CollectPlay()
    {
        collect.Play();
    }
}

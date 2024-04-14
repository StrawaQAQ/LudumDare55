using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour,getDamage
{
    [HideInInspector]public Animator anim;
    [Header("生命值")]
    public float health;
    private Rigidbody2D rb;
    Vector2 moveInput;
    public PlayerInput input;
    [Header("移动速度")]
    public float speed;
    public bool enableMove;
    [Header("拾取的item数量")]
    public int getNum;
    public bool enableCall;
    [Header("键盘监听")]
    public GameObject kl;
    private KeyboradListen Kl;
    public CapsuleCollider2D cap;
    public int damage;
    public float MaxTime;
    public Vector2 attackRange;
    void Start()
    {
        input = new PlayerInput();
        input.Enable();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Kl = kl.GetComponent<KeyboradListen>();
        cap = GetComponent<CapsuleCollider2D>();
        enableMove = true;
        enableCall = false;
        attackRange = new Vector2(4f, 2f);
    }
    void FixedUpdate()
    {
        OnMove();
        
        anim.SetFloat("Horizontal", moveInput.x);
        anim.SetFloat("Vertical", moveInput.y);
        anim.SetFloat("Magnitude", moveInput.magnitude);
    }
    private void Update()
    {
        ChangeInput();
    }
    private void OnMove()
    {
        if (enableMove)
        {
            moveInput = input.Player.Move.ReadValue<Vector2>();
            rb.velocity = new Vector2(moveInput.x * speed * Time.deltaTime, moveInput.y * speed * Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector2.zero;
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
        if(health < 0)
        {
            Destroy(gameObject);
        }
    }
}

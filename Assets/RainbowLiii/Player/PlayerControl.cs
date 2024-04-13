using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public Animator anim;
    Rigidbody2D rb;
    Vector2 moveInput;
    public PlayerInput input;
    public float speed;
    public bool enableMove;
    public bool enableCall;
    public GameObject kl;
    void Start()
    {
        input = new PlayerInput();
        input.Enable();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enableMove = true;
        enableCall = false;
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
            if (enableMove)
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
}

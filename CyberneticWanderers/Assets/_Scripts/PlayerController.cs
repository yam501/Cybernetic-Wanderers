using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Todo Animations

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    Rigidbody2D rb;
    [HideInInspector] public Vector2 moveDir;
    public SpriteRenderer playerSprite;
    [HideInInspector] public float lastHorizontalVector;
    [HideInInspector] public float lastVerticalVector;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        InputManager();

        //if(Input.GetAxis("Horizontal") < 0)         //�����
        //{
        //    playerSprite.flipX = true;
        // }
        // if (Input.GetAxis("Horizontal") > 0)   //������
        // {
        //     playerSprite.flipX = false;
        // }
    }

    private void FixedUpdate()
    {
        Move();
    }


    void InputManager()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY);

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
        }

        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}
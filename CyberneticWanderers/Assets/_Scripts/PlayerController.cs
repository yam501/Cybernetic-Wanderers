using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Todo Animations

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 10f;
    Rigidbody2D rb;
    Vector2 moveDir;
    public SpriteRenderer playerSprite;


    [Header("Аниматор игрока")]
    public Animator animator;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        playerSprite = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        InputManager();      

       //if(Input.GetAxis("Horizontal") < 0)         //влево
       //{
       //    playerSprite.flipX = true;
       // }
       // if (Input.GetAxis("Horizontal") > 0)   //вправо
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

        animator.SetBool("isMoving", (Mathf.Abs(moveX) > 0 || Mathf.Abs(moveY) > 0) ? true : false);

        animator.SetFloat("directionX", moveX);
        moveDir = new Vector2(moveX, moveY);
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}

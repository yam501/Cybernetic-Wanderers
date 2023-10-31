using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Todo Animations

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 20f;
    Rigidbody2D playerRB;
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lastMovedVector; 
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    public SpriteRenderer playerSprite;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>(); 
        playerSprite = GetComponent<SpriteRenderer>();
        lastMovedVector = new Vector2(1, 0f);
    }

    
    void Update()
    {
        InputManager();      
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
            lastMovedVector = new Vector2(moveDir.x, 0f);
        }
        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2 (0f, moveDir.y);
        }

        if (moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector); // while moving
        }
    }

    void Move()
    {
        playerRB.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}

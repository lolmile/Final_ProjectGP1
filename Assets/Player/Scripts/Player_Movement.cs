using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private Animator anim;

    public float moveSpeed = 7;

    private float dirX = 0f;
    private float dirY = 0f;

    private enum MovementState { Left, Right, Up, Down }
    private MovementState currentMovementState;

    private bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // Move the player based on input values
        if (isMoving)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, dirY * moveSpeed);
            isMoving = false;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        ChangeAnimation();
    }

    private void ChangeAnimation()
    {
        if (dirX == 0 && dirY == 0)
        {
            anim.SetInteger("movementState", 5);
            switch (currentMovementState)
            {
                case MovementState.Up:
                    anim.SetInteger("idleState", 1);
                    break;
                case MovementState.Down:
                    anim.SetInteger("idleState", 2);
                    break;
                case MovementState.Left:
                    anim.SetInteger("idleState", 0);
                    break;
                case MovementState.Right:
                    anim.SetInteger("idleState", 0);
                    break;
                default:
                    break;
            }
        }
        else
        {
            isMoving = true;
            if (dirX > 0)
            {
                currentMovementState = MovementState.Right;
                anim.SetInteger("movementState", 0);
                sprite.flipX = true;
            }
            else if (dirX < 0)
            {
                currentMovementState = MovementState.Left;
                anim.SetInteger("movementState", 0);
                sprite.flipX = false;
            }

            if (dirY > 0)
            {
                currentMovementState = MovementState.Up;
                anim.SetInteger("movementState", 1);
                sprite.flipX = false;
            }
            else if (dirY < 0)
            {
                currentMovementState = MovementState.Down;
                anim.SetInteger("movementState", 2);
                sprite.flipX = false;
            }
        }
    }
}

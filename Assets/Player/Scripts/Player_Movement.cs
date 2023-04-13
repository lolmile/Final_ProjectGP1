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

    private enum MovementState { idle, running, runningUp, runningDown }

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
        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");

        Debug.Log(dirX+ ", " + dirY);

        rb.velocity = new Vector2(dirX * moveSpeed, dirY * moveSpeed);

    }

    private void UpdateAnimation()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (dirY > 0f)
        {
            state = MovementState.runningUp;
        }
        else if (dirY < 0f)
        {
            state = MovementState.runningDown;
        }

        anim.SetInteger("state", (int)state);
    }
}

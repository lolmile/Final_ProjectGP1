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

    [SerializeField] ParticleSystem runDust;
    [SerializeField] AudioSource runningSound;

    public float moveSpeed = 7;

    private float dirX = 0f;
    private float dirY = 0f;

    public enum MovementState { Left, Right, Up, Down }
    public MovementState currentMovementState;

    SpriteRenderer[] allchildren;

    private bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        allchildren = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");

        if (isMoving == true)
        {
            CreateRunDust();
            PlayWalkingSound();
        }
        else
        {
            runDust.Stop();
            runningSound.Stop();
        }
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
            switch (currentMovementState)
            {
                case MovementState.Up:
                    anim.SetInteger("state", 5);
                    break;
                case MovementState.Down:
                    anim.SetInteger("state", 3);
                    break;
                case MovementState.Left:
                    anim.SetInteger("state", 4);
                    break;
                case MovementState.Right:
                    anim.SetInteger("state", 4);
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
                anim.SetInteger("state", 1);
                sprite.flipX = true;
                foreach (SpriteRenderer child in allchildren)
                {
                    child.flipX = true;
                }
            }
            else if (dirX < 0)
            {
                currentMovementState = MovementState.Left;
                anim.SetInteger("state", 1);
                sprite.flipX = false;
                foreach (SpriteRenderer child in allchildren)
                {
                    child.flipX = false;
                }
            }

            if (dirY > 0)
            {
                currentMovementState = MovementState.Up;
                anim.SetInteger("state", 2);
                sprite.flipX = false;
                foreach (SpriteRenderer child in allchildren)
                {
                    child.flipX = false;
                }
            }
            else if (dirY < 0)
            {
                currentMovementState = MovementState.Down;
                anim.SetInteger("state", 0);
                sprite.flipX = false;
                foreach (SpriteRenderer child in allchildren)
                {
                    child.flipX = false;
                }
            }
        }
    }

    private void CreateRunDust()
    {
        if (!runDust.isPlaying)
        {
            runDust.Play();
        }
    }

    private void PlayWalkingSound()
    {
        if (!runningSound.isPlaying)
        {
            runningSound.Play();
        }
    }
}

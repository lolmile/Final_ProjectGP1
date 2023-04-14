using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princessMovement : MonoBehaviour
{

 private Rigidbody2D rb;
 private Animator anim;
private CapsuleCollider2D coll;
private SpriteRenderer rbSprite;
   private float dirX;

private float dirY;

    [SerializeField] private float moveSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {

          rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
rbSprite = GetComponent<SpriteRenderer>();
 coll = GetComponent<CapsuleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {


dirX = Input.GetAxisRaw("Horizontal");
dirY = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(dirX * moveSpeed, dirY * moveSpeed);



         //MovementState state;
        if (rb.velocity.x < 0.1f)
        {

           // state = MovementState.running;
            rbSprite.flipX = false;


        }
        else if (rb.velocity.x  > -0.1f)
        {

           // state = MovementState.running;
            rbSprite.flipX = true;

        }

        
    }
}

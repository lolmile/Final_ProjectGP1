using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonsMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = .5f;
    private Animator anim;
    private GameObject target;
    private enum MovementState { idle, walk, strike, attack, crouch, crouchAttack, die, dizzy, flyingKick, hurt, jumpAttack, win };
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("state", (int)MovementState.idle); // set the initial animation state to idle
    }
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        // Get the direction of movement
        Vector3 direction = waypoints[currentWaypointIndex].transform.position - transform.position;

        // Flip the sprite if moving to the left
        if (direction.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }


        // Move the enemy towards the waypoint
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);

    }
    private void UpdateAnimationState()
    {
        if (transform.position != waypoints[currentWaypointIndex].transform.position)
        {
            anim.SetInteger("state", (int)MovementState.walk);
        }
        else
        {
            anim.SetInteger("state", (int)MovementState.idle);
        }
    }
    public void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
        UpdateAnimationState();
    }
    //Set the attack animation
    public void onTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Princess")
        {
            target = collision.gameObject;
            anim.SetInteger("state", (int)MovementState.attack);
        }
    }

}

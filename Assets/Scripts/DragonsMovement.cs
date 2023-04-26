using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonsMovement : MonoBehaviour
{
    public GameObject princess;
    public float dragonSpeed = 5f;
    public float attackDistance = 100f;
    public enum DragonAnim { Idle, Walk, Attack, Hurt }
    public DragonAnim currentAnim;

    private Animator animator;
    private bool isFollowing = false;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("state", (int)currentAnim);
    }

    void Update()
    {
        // calculate distance between dragon and princess
        float distance = Vector3.Distance(transform.position, princess.transform.position);

        if (distance < attackDistance)
        {
            // if princess is close enough, start following her
            isFollowing = true;
            isAttacking = false;
            SetAnimation(DragonAnim.Walk);
        }
        else if (isFollowing)
        {
            // if princess is out of range, stop following her
            isFollowing = false;
            SetAnimation(DragonAnim.Idle);
        }

        if (isFollowing)
        {
            // move dragon towards princess
            Vector3 direction = (princess.transform.position - transform.position).normalized;
            transform.Translate(direction * dragonSpeed * Time.deltaTime, Space.World);

            // face the direction of movement
            FlipTowards(direction);
            SetAnimation(DragonAnim.Walk);

            // check if dragon is close enough to attack
            if (distance <= attackDistance / 2)
            {
                isAttacking = true;
                SetAnimation(DragonAnim.Attack);
            }
            else
            {
                isAttacking = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // check if princess has entered attack area
        if (other.gameObject == princess)
        {
            isFollowing = true;
            SetAnimation(DragonAnim.Attack);
            Debug.Log("Princess has entered attack area!");

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // check if princess has exited attack area
        if (other.gameObject == princess)
        {
            isFollowing = false;
            SetAnimation(DragonAnim.Attack);
            Debug.Log("Princess has escaped!");
        }

    }

    void SetAnimation(DragonAnim anim)
    {
        if (currentAnim == anim) return;
        animator.SetInteger("state", (int)anim);
        currentAnim = anim;
    }

    void FlipTowards(Vector3 targetDirection)
    {
        if (targetDirection.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}




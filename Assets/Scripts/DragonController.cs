using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float followRange = 1f;
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] float attackNext = 0f;
    public int damage = 10;
    private GameObject target;
    SpriteRenderer sprite;
    Transform AttackRangerCenter;

    [SerializeField] PlayerCombat playerScript;

    //Get gameobject child firepoint

    //Get playercontroller script

    //Follow range and then attack range
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        AttackRangerCenter = GetComponentInChildren<Transform>();
        sprite= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > attackNext)
        {
            DragonMove();
        }
    }
    private void DragonMove()
    {

            // Calculate the distance between the enemy and the player
            float distance = Vector3.Distance(AttackRangerCenter.position, target.transform.position);

            // If the distance is greater than the follow range, set the enemy state to idle
            if (distance > followRange)
            {
                anim.SetInteger("state", 0);
            }
            // If the distance is less than or equal to the follow range but greater than the attack range, set the enemy state to walk
            else if (distance < followRange && distance > attackRange)
            {
                anim.SetInteger("state", 1);

                // Move the dragon towards the player
                gameObject.transform.position = Vector2.MoveTowards(AttackRangerCenter.transform.position, playerScript.attackPoint.transform.position, m_speed * Time.deltaTime);

                // Flip the dragon if the player is to the left or right
                if (target.transform.position.x < transform.position.x)
                {
                    sprite.flipX = true;
                }
                else if (target.transform.position.x > transform.position.x)
                {
                    sprite.flipX = false;
                }
            }

        else if (!playerScript.isDead)
        {
            //cooldown for 1 second
            Collider2D[] hits = Physics2D.OverlapCircleAll(AttackRangerCenter.position, attackRange);
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject.CompareTag("Player"))
                {
                        // Attack the player
                        anim.SetInteger("state", 2);
                        playerScript.TakeDamage(damage);
                        attackNext = Time.time + attackCooldown;
                        break;
                }
            }
        }
    }


    private void FollowThePlayer()
    {
        // if the player is in the attack range of the enemy, the enemy will follow the player
        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
        {
            //if the player is to the left of the enemy
            if (target.transform.position.x < transform.position.x)
            {
                //move the enemy to the left

                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, m_speed * Time.deltaTime);
                //if the is player is up of the enemy the enemy will move up to the player position

                //flip the enemy to the left
                transform.localScale = new Vector3(-1, 1, 1);
            }
            //if the player is to the right of the enemy
            else if (target.transform.position.x > transform.position.x)
            {
                //move the enemy to the right
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, m_speed * Time.deltaTime);
                //flip the enemy to the right
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        //if the animation on attck the dragon doesn't move



    }


}

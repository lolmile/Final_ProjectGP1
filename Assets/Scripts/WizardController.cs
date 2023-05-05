using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] GameObject target;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float followRange = 1f;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] float attackNext = 0f;
    public int damage = 10;


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
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > attackNext)
        {
            // attackNext = Time.time + attackCooldown;
            //  playerScript.TakeDamage(damage);
            DragonMove();
        }


    }
    private void DragonMove()
    {

        // Check if target is null
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            // Calculate the distance between the enemy and the player
            float distance = Vector3.Distance(transform.position, target.transform.position);

            // If the distance is greater than the follow range, set the enemy state to idle
            if (distance > followRange)
            {
                anim.SetInteger("BossState", 0);
            }
            // If the distance is less than or equal to the follow range but greater than the attack range, set the enemy state to walk
            else if (distance <= followRange && distance > attackRange)
            {
                anim.SetInteger("BossState", 1);

                // Move the dragon towards the player
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, m_speed * Time.deltaTime);

                // Flip the dragon if the player is to the left or right
                if (target.transform.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (target.transform.position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }

            // If the distance is less than or equal to the attack range, set the enemy state to attack
            else if (distance <= attackRange)
            {
                // anim.SetInteger("state", 1);

                //cooldown for 1 second



                attackCooldown = 1f;
                anim.SetInteger("BossState", 2);
                playerScript.TakeDamage(damage);

                attackNext = Time.time + attackCooldown;


            }

        }
    }
}


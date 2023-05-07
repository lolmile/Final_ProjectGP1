using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private Enemy enemyScript;

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float followRange = 1f;
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] float attackNext = 0f;
    [SerializeField] AudioSource attackSound;
    [SerializeField] Collider2D princessPrison;
    [SerializeField] GameObject prison;
    [SerializeField] GameObject target;

    public int damage = 10;
    SpriteRenderer sprite;
    private Transform AttackRangeCenter;
    public bool isAttacking = false;
    public bool isAttacked = false;


    [SerializeField] PlayerCombat playerScript;

    //Get gameobject child firepoint

    //Get playercontroller script

    //Follow range and then attack range
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        AttackRangeCenter = transform.Find("AttackRangeCenter");
        sprite = GetComponent<SpriteRenderer>();
        enemyScript = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        WizardMove();

        if (enemyScript.isDead)
        {
            princessPrison.enabled = false;
            prison.SetActive(false);
            target = null;
        }
    }
    private void WizardMove()
    {
        if (!enemyScript.isDead)
        {
            // Calculate the distance between the enemy and the player
            float distance = Vector3.Distance(AttackRangeCenter.position, target.transform.position);

            Collider2D[] hits = Physics2D.OverlapCircleAll(AttackRangeCenter.position, attackRange);

            // If the distance is greater than the follow range, set the enemy state to idle
            if (distance > followRange || distance < attackRange)
            {
                anim.SetInteger("BossState", 0);
            }
            // If the distance is less than or equal to the follow range but greater than the attack range, set the enemy state to walk
            else if (distance < followRange && distance > attackRange && hits.Length != 0)
            {
                anim.SetInteger("BossState", 1);

                // Move the dragon towards the player
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, playerScript.attackPoint.transform.position, m_speed * Time.deltaTime);

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

            if (!playerScript.isDead && hits.Length > 0 && !enemyScript.isDead && !enemyScript.isAttacked)
            {
                if (Time.time > attackNext)
                {
                    foreach (Collider2D hit in hits)
                    {
                        if (hit.gameObject.CompareTag("Player"))
                        {
                            // Attack the player
                            anim.SetTrigger("attack");
                            playerScript.TakeDamage(damage);
                            attackSound.Play();
                            attackNext = Time.time + attackCooldown;
                            break;
                        }
                    }
                }

            }
        }

    }
    private void OnDrawGizmosSelected()
    {
        if (AttackRangeCenter != null)
        {
            Gizmos.DrawWireSphere(AttackRangeCenter.position, attackRange);
        }
    }

    public void IsAttacking()
    {
        isAttacking = true;
    }
    public void IsNotAttacking()
    {
        isAttacking = false;
    }
}

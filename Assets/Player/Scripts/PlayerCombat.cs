using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    private Player_Movement movement;
    private Rigidbody2D rb;

    [SerializeField] AudioSource attackSound;
    public Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] int attackDamage = 25;
    [SerializeField] float attackRate = 0.5f;
    [SerializeField] private TextMeshProUGUI text;
    private float nextAttackTime = 0f;
    public LayerMask enemyLayers;
    public bool isDead = false;

    public int totalKilled = 0;
    public int maxHealth = 100;
    public int currenthealth;

    public healthBar healthBar;
    public Player_Movement Player_Movement;

    private void Start()
    {
        movement = GetComponent<Player_Movement>();
        rb= GetComponent<Rigidbody2D>();

        currenthealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Vector2.zero;
                attackSound.Play();
                if (movement.currentMovementState == Player_Movement.MovementState.Left)
                {
                    attackPoint.localPosition = new Vector3(-0.9f, 0.42f);
                    animator.SetTrigger("SattackSide");
                    Attack();
                }
                if (movement.currentMovementState == Player_Movement.MovementState.Right)
                {
                    attackPoint.localPosition = new Vector3(0.64f, 0.42f, 0);
                    animator.SetTrigger("SattackSide");
                    Attack();
                }
                if (movement.currentMovementState == Player_Movement.MovementState.Up)
                {
                    attackPoint.localPosition = new Vector3(0, 1.09f, 0);
                    animator.SetTrigger("SattackUp");
                    Attack();
                }
                if (movement.currentMovementState == Player_Movement.MovementState.Down)
                {
                    attackPoint.localPosition = new Vector3(0, -0.28f, 0);
                    animator.SetTrigger("SattackDown");
                    Attack();
                }
                nextAttackTime = Time.time + 1f /attackRate;
            }
        }

    }

    public void StartMovement()
    {
        // Disable the movement script here
        GetComponent<Player_Movement>().enabled = true;
    }

    public void StopMovement()
    {
        // Disable the movement script here
        GetComponent<Player_Movement>().enabled = false;
    }

    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        healthBar.SetHealth(currenthealth);
        animator.SetTrigger("PlayerHit");

        if(currenthealth <= 0)
        {
            Die();
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    void Die()
    {
        isDead = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
        animator.SetTrigger("PlayerDie");
    }

    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void UpKillCount()
    {
        totalKilled++;
        text.text = totalKilled.ToString();
        attackDamage += 5;
        Player_Movement.moveSpeed += 0.2f;
    }
}

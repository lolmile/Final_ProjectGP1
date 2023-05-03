using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] PlayerCombat killcount;
    public int maxHealth = 100;
    private int currentHealth;
    private Collider2D col;

    void Start()
    {
        currentHealth = maxHealth;
        col = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //PLAY ANIMATION

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //DIE ANIMATION
        col.enabled= false;
        Debug.Log("DEAD");
        killcount.UpKillCount();
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}

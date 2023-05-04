using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] PlayerCombat killcount;
    public int maxHealth = 100;
    private int currentHealth;
    private Animator anim;
    private Collider2D coll;


    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //PLAY ANIMATION

        if (currentHealth <= 0)
        {
            Die();
            OnDestroy();

        }
        else
        {
            anim.SetTrigger("Hurt");
        }

    }

    public void Die()
    {
        //DIE ANIMATION
        anim.SetTrigger("Death");
        killcount.UpKillCount();
    }

    public void OnDestroy()
    {
        Destroy(gameObject, .5f);

    }

    public void Disable()
    {
        coll.enabled = false;
    }


}

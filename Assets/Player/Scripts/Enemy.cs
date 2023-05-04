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
    private bool isDead;


    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        coll = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            OnDestroy();

        }
        else
        {
            anim.SetTrigger("hurt");
        }

    }

    public void Die()
    {
        //DIE ANIMATION
        anim.SetTrigger("Death");

        isDead = true;
        anim.SetTrigger("death");
        //GetComponent<DragonController>().enabled = false;
        //GetComponent<MeleeAttack>().enabled = false;
        Destroy(gameObject, 1f);

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

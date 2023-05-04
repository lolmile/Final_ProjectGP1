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
        }
        else
        {
            anim.SetTrigger("hurt");
        }
    }

    public void Die()
    {

        isDead = true;
        anim.SetTrigger("death");
        //GetComponent<DragonController>().enabled = false;
        //GetComponent<MeleeAttack>().enabled = false;
        Destroy(gameObject, 1f);

        killcount.UpKillCount();
    }




}

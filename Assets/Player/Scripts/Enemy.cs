using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] PlayerCombat killcount;
    [SerializeField] AudioSource hitSound;
    [SerializeField] AudioSource dieSound;

    public int maxHealth = 100;
    private int currentHealth;
    private Animator anim;
    private Collider2D coll;
    private Rigidbody2D rb;
    public bool isDead;


    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                anim.SetTrigger("hurt");
                hitSound.Play();
            }
        }
    }

    private void Die()
    {
        isDead = true;
        dieSound.Play();
        anim.SetTrigger("death");
       
        killcount.UpKillCount();
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    public void Disable()
    {
        coll.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }


}

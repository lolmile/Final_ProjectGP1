using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] GameObject target;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float attackCooldown = 1f;

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

        DragonMove();
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
            Debug.Log("Distance: " + distance);

            //If the distance is greater than the attack range
            if (distance <= attackRange)
            {
                //Set the enemy state to idle
                anim.SetInteger("state", 2);
            }

            //If the distance is less than the attack range

            //if the player is to the left of the enemy



        }
    }
}

//if the animation on attck the dragon doesn't move





// public void OnTriggerEnter2D(Collider2D collision)
// {
//     if (collision.gameObject.tag == "Player")
//     {
//         //attack the player
//         anim.SetInteger("state", 2);
//         Debug.Log("Enemy is attacking");
//         //  Shoot();
//     }
// }




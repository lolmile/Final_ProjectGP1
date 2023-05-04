using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] GameObject target;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] GameObject fireballPrefab;
    //Get gameobject child firepoint
    [SerializeField] Transform firePoint;
    //Get playercontroller script


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
            if (distance > attackRange)
            {
                //Set the enemy state to idle
                anim.SetInteger("state", 0);
            }
            else if (distance == attackRange)
            {
                anim.SetInteger("state", 1);
            }
            //If the distance is less than the attack range
            else
            {
                //set the enemy to Walk and Attack after 1 second
                anim.SetInteger("state", 1);

                //cooldown for 1 second
                attackCooldown -= Time.deltaTime;
                Debug.Log("attackCooldown: " + attackCooldown);
                if (attackCooldown <= 0)
                {
                    //reset the cooldown
                    attackCooldown = 1f;
                    Debug.Log("Enemy is cooldown");
                    //attack the player
                    anim.SetInteger("state", 2);
                    Debug.Log("Enemy is attacking");
                    //  Shoot();


                }

                //if the player is to the left of the enemy
                FollowThePlayer();


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



    }
    public void Shoot()
    {
        // Instantiate the fireball prefab
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

        // Get the fireball script
        //let the fire ball start from the position of the fire point
        fireballPrefab.transform.position = firePoint.position;
        //let the fire ball start from the position of the fire point
        FireController fire = fireball.GetComponent<FireController>();
        fire.SetDirection(transform.localScale.x);

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //attack the player
            anim.SetInteger("state", 2);
            Debug.Log("Enemy is attacking");
            //  Shoot();
        }
    }


}

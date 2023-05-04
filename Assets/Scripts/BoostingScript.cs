using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostingScript : MonoBehaviour
{
    private SpriteRenderer sprite;
    private TrailRenderer trail;
    [SerializeField] float boostCooldown = 1f;
    [SerializeField] float nextBosst = 0f;

    private float starting;
     private float speed = 2.0f;

    public Player_Movement playerScript;
    public float height = 1.0f;

    private Vector3 startPosition;

    private bool triggered = false;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
        trail = GameObject.FindGameObjectWithTag("Player").GetComponent<TrailRenderer>();
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (triggered)
        {
            
        if (Time.time >= starting + 5f)
        {
            playerScript.moveSpeed = 7f;
            trail.enabled = false;

            Destroy(gameObject);
        }
        }
        float newY = Mathf.Sin(Time.time * speed) * height + startPosition.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    void OnTriggerEnter2D(Collider2D other)
    {



        if (other.gameObject.CompareTag("Player"))
        {
            sprite.enabled = false;

            playerScript.moveSpeed = 15f;

            trail.enabled = true;

            starting = Time.time;

            triggered = true;

        }
    }
}

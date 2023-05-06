using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostingScript : MonoBehaviour
{
    private SpriteRenderer sprite;
    private TrailRenderer trail;
    [SerializeField] float boostCooldown = 1f;
    [SerializeField] float nextBosst = 0f;
    [SerializeField] AudioSource boostSound;

    private BoxCollider2D boxCollider2D;
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
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (triggered)
        {        
            if (Time.time >= starting)
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
            boostSound.Play();
            sprite.enabled = false;
            boxCollider2D.enabled = false;

            playerScript.moveSpeed = 15f;

            trail.enabled = true;

            starting = Time.time + 4f;

            triggered = true;
        }
    }
}

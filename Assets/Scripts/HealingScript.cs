using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingScript : MonoBehaviour
{
    public PlayerCombat playerScript;

    public int amountHealed = 10;
    private float speed = 2.0f;
    private  float height = 1.0f;
    private SpriteRenderer image;
    private Collider2D col;
    [SerializeField] GameObject particleSystemPrefab;
    [SerializeField] AudioSource healSound;

    private Vector3 startPosition;


private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
    }

    void Start()
    {
        startPosition = transform.position;
        image = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) * height + startPosition.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject.CompareTag("Player"))
        {
            healSound.Play();
            playerScript.Heal(amountHealed);
            Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
            image.sprite = null;

            col.enabled= false;
                    
            Destroy(gameObject, 2f);
        }
    }
}

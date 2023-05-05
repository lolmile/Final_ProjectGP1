using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingScript : MonoBehaviour
{
    public PlayerCombat playerScript;

    public int amountHealed = 10;
    private float speed = 2.0f;
    private  float height = 1.0f;
    [SerializeField] GameObject particleSystemPrefab;
    private Vector3 startPosition;


private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
    }

    void Start()
    {
        startPosition = transform.position;
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
            
            playerScript.Heal(amountHealed);
            Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
                    
            Destroy(gameObject);

            
        }
    }
}

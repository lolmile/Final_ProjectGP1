using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] float speed;
    private float lifetime = 1;
    private float direction;
    private Rigidbody2D rb;
    [SerializeField] float attackCooldown = 1f;
    private BoxCollider2D boxCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Set the fireball movement, speed and direction
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 2) gameObject.SetActive(false);
    

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the fireball collides with the player
        if (collision.gameObject.tag == "Player")
        {
            // Destroy the fireball
            Destroy(gameObject, 1f);
        }
    }
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);

        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    // Set the position of the fireball to be at the position of the fire point
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

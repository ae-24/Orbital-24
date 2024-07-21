using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D myRigidbody;
    PlayerMovement player;
    float xSpeed; // Horizontal speed and direction of bullet

    void Start()
{
    myRigidbody = GetComponent<Rigidbody2D>();
    if (myRigidbody == null)
    {
        Debug.LogError("Rigidbody2D component not found on Enemy.");
    }
    player = FindObjectOfType<PlayerMovement>();
    if (player != null)
    {
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }
    else
    {
        Debug.LogError("PlayerMovement component not found.");
    }
}

    void Update()
    {
        transform.localScale = new Vector2(Mathf.Sign(player.transform.localScale.x), 1f);
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            EnemyMovement enemyMovement = other.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.TakeDamage(1);
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

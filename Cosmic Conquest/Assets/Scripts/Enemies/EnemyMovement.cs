using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int health;
    Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-Mathf.Sign(myRigidbody.velocity.x), 1f);
    }

    public void TakeDamage(int damage) 
    {
        health -= damage;
        Debug.Log("Damage Taken");
        if(health <= 0) 
        {
            EnemyState enemyState = GetComponent<EnemyState>();
            if (enemyState != null)
            {
                enemyState.Died();
            }
        }
    }
}

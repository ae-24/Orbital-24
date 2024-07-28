using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int health;
    [SerializeField] GameObject bloodEffect;
    [SerializeField] float startDazedTime;

    Animator myAnimator;
    Rigidbody2D myRigidbody;
    float dazedTime;
    float dazedSpeed = 0f;
    float currentSpeed;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        if (myRigidbody == null)
        {
            Debug.LogError("Rigidbody2D not found on " + gameObject.name);
            return;
        }

        currentSpeed = moveSpeed;
    }

    void Update()
    {
        if (myRigidbody == null) return;

        if (dazedTime <= 0)
        {
            myAnimator.SetBool("isDazed", false);
            moveSpeed = currentSpeed;
        }
        else
        {
            myAnimator.SetBool("isDazed", true);
            moveSpeed = dazedSpeed;
            dazedTime -= Time.deltaTime;
        }

        myRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Bullet") { return; }
        moveSpeed = -moveSpeed;
        currentSpeed = -currentSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-Mathf.Sign(myRigidbody.velocity.x), 1f);
    }

    // public void TakeDamage(int damage)
    // {
    //     dazedTime = startDazedTime;
    //     Instantiate(bloodEffect, transform.position, Quaternion.identity);
    //     health -= damage;

    //     if (health <= 0)
    //     {
    //         EnemyState enemyState = GetComponent<EnemyState>();
    //         if (enemyState != null)
    //         {
    //             enemyState.Died();
    //         }
    //     }
    // }
    public void TakeDamage(int damage)
    {
        if (this == null || gameObject == null)
            return;

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (this == null || gameObject == null)
            return;

        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        PlayerPrefs.SetInt(GetComponent<EnemyState>().enemyID, 0);
    }
}
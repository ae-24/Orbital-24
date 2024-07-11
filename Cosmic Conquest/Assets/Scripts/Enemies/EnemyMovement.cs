using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int health;
    Rigidbody2D myRigidbody;
    public string enemyID;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        LoadState();
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

    public void SaveState()
    {
        PlayerPrefs.SetInt(enemyID, 1);
        PlayerPrefs.Save();
    }

    void LoadState()
    {
        if (PlayerPrefs.GetInt(enemyID, 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    public static void ResetAllEnemies()
    {
        EnemyManager.Instance?.ResetAllEnemies();
    }

    public void TakeDamage(int damage) 
    {
        health -= damage;
        Debug.Log("damage TAKEN");
        if(health <= 0) {
            Destroy(gameObject);
        }
    }
}

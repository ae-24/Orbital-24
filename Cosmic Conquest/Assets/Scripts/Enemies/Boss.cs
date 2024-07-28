using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] Slider healthBar;
    [SerializeField] GameObject bloodEffect;
    private float timeBtwDamage = 1.5f;
    
    //public Animator camAnim;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();      
    }

    void Update()
    {
        if (health <= 15) {
            anim.SetTrigger("phaseTwo");
        }

        if (health <= 0) {
            anim.SetTrigger("Death");
        }

        if (timeBtwDamage > 0) {
            timeBtwDamage -= Time.deltaTime;
        }

        healthBar.value = health;
    }

    public void TakeDamage(int damage) 
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
        //Debug.Log("Damage Taken");
        if(health <= 0) 
        {
            Invoke("Dead", 1f);
        }
    }

    void Dead () 
    {
        EnemyState enemyState = GetComponent<EnemyState>();
        if (enemyState != null)
        {
            enemyState.Died();
        }
    }
}

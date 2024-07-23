using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovementMelee : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 24f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] Vector2 deathKick = new Vector2 (0f, 0f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    [SerializeField] Transform attackPos;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask whatIsEnemies;
    [SerializeField] int damage;
    [SerializeField] float startTimeBetweeenAttack;
    
    float timeBetweenAttack;
    float gravityScaleAtStart;
    bool isAlive = true;
    bool canAttack = true;
    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    SpriteRenderer mySpriteRenderer;
    //public Vector3 playerPos;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

  
    void Update()
    {
        if(!isAlive){ return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();

        if(timeBetweenAttack <= 0) {
            canAttack = true;            
        } else {
            timeBetweenAttack -= Time.deltaTime;
        }
        //transform.position = playerPos;
    }

    void OnFire(InputValue values) 
    {
        if(!PauseMenu.isPaused) 
        {
            Debug.Log("Attacking");
            if(!isAlive) {return;}
            if(!canAttack) {Debug.Log("cant attack") ;return;}
            canAttack = false;
            timeBetweenAttack = startTimeBetweeenAttack;
            Debug.Log("attackinggg");
            myAnimator.SetTrigger("Attacking");
            Invoke("Attack", 0.7f);          
        }
    }

    void Attack()
    {
         Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
         for (int i = 0; i < enemiesToDamage.Length; i++) {
            enemiesToDamage[i].GetComponent<EnemyMovement>().TakeDamage(damage);
         }
         myAnimator.SetTrigger("notAttacking");
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }


    void OnMove(InputValue value)
    {
        if(!isAlive){ return;}
        moveInput = value.Get<Vector2>();

    } 

    void OnJump(InputValue value) 
    {
        if(!isAlive){ return; }
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            return;
        }
    
        if(value.isPressed){
            myRigidBody.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

    void Run ()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);

    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed){
            transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    void ClimbLadder() {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) {
            Vector2 climbVelocity = new Vector2 (myRigidBody.velocity.x, moveInput.y * climbSpeed);
            myRigidBody.velocity = climbVelocity;
            myRigidBody.gravityScale = 0f;
            bool playerIsClimbing = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("isClimbing", playerIsClimbing);
        } else {
            myRigidBody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
        }       
    }

    //Updates isAlive to determine player mortality upon player collision with objects
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            isAlive = false;
        }
    }

    public void Die() 
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards"))) {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            mySpriteRenderer.color = new Color (2f,1f,1f);
            Invoke("Death", 0.7f);

        }
    }

    void Death() 
    {
        myRigidBody.velocity = deathKick;
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
}
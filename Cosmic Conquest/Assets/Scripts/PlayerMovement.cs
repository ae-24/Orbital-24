using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 24f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] Vector2 deathKick = new Vector2 (10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    float gravityScaleAtStart;
    bool isAlive = true;
    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    SpriteRenderer mySpriteRenderer;
    //public Vector3 playerPos;
    private LimitedVisionManager limitedVisionManager;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        gravityScaleAtStart = myRigidBody.gravityScale;

        limitedVisionManager = FindObjectOfType<LimitedVisionManager>();
    }

  
    void Update()
    {
        if(!isAlive){ return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
        //transform.position = playerPos;
    }

    void OnFire(InputValue values) 
    {
        if(!PauseMenu.isPaused) 
        {
            if(!isAlive){ return;}
            myAnimator.SetTrigger("Shooting");
            Invoke("ShootBullet", 0.3f);          
        }
    }

    void ShootBullet()
    {
         Instantiate(bullet, gun.position, transform.rotation);
         myAnimator.SetTrigger("notShooting");
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

    void Die() 
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards"))) {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            mySpriteRenderer.color = new Color (255f,0f,0f);
            myRigidBody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LimitedVision"))
        {
            limitedVisionManager.TriggerLimitedVision(5f); // Trigger limited vision for 5 seconds
        }
    }
}

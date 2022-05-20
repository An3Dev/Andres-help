//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //all the private instance variables 
    //SerializeField allows change of the varible inside unity itself

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    bool facingRight = true;
    [SerializeField] SpriteRenderer keag;
    [SerializeField] private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;



    void Awake()
    {
        //grab references for rigidbody and animator from object 
        body = GetComponent<Rigidbody2D>();
        //keag = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    
     void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        //flips the main character sprite render based on direction
        if (horizontalInput > 0 && !facingRight) {
            Flip();
        } else if (horizontalInput < 0 && facingRight) {
            Flip();
        }
        

        //set animation
        //changes animation based on action
        //walking
        anim.SetBool("walk", horizontalInput != 0);
        //idle
        anim.SetBool("grounded", isGrounded());

        //was supposed to be a wall jump, couldnt get it to work so instead became 
        //a climbing wall mechanism
        if (wallJumpCooldown > .2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            //if the player is on the wall
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                if (Input.GetKey(KeyCode.W))
                {
                    Jump();
                }
                //if player is not on wall                                                                                                                                                                                      body.velocity = Vector2.zero;
            } else {
                body.gravityScale = 5;
                //allows player to jump
                if (Input.GetKey(KeyCode.W))
                {
                    Jump();
                }
            }
        } else {
            wallJumpCooldown += Time.deltaTime;
        }

    }


    
    //
    private void Jump() {
        if (isGrounded())
        {
            //controls the jump as well as the animation
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        //if the player is on the wall and not grounded, climbing mechanism 
        else if(onWall() && !isGrounded()) {
            if (horizontalInput == 0){
                int directedByAndres = isFlip() ? -1 : 1;
                body.velocity = new Vector2(directedByAndres * -10, 0);
            }else{
                int directedByAndres = isFlip() ? -1 : 1;
                body.velocity = new Vector2(directedByAndres * 6, 6);
                wallJumpCooldown = 0;
            }
        }
    }

    //flips the player
    void Flip() {
        Vector3 currentScale = transform.localScale;
        keag.flipX = facingRight;
        transform.localScale = currentScale;

        facingRight = !facingRight;
    }
    //returns bool if the player is flipped
    public bool isFlip() {
        return keag.flipX;
    }
    //returns if the player is grounded using a raycast, if is ground then it allows 
    //jump to occur
    private bool isGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 
            0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    //return bool if player is on either right or left wall, allows player to climb
    //the wall
    private bool onWall()
    {
        int directedByAndres = isFlip() ? -1 : 1;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size,
            0, new Vector2(directedByAndres, 0), 0.5f, wallLayer);
        //print(raycastHit.collider);
        return raycastHit.collider != null;
        
    }

    //if the player is stationary, then it will allow the player to attack
    //with his gun
    public bool canAttack() { 
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

}

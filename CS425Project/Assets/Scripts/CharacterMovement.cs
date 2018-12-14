using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpSpeed = 250f;
    public GameObject character;
    private Rigidbody2D playerRB;
    private Animator animator;
    private SpriteRenderer rend;
    private bool isFlipped;

    private bool airborne;

    public int health = 100;
    
	// Use this for initialization
	void Start ()
    {
        playerRB = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
	}

    void Update()
    {
        if(health <= 0)
        {
            animator.SetBool("isDead", true);
        }
        if(Input.GetButtonDown("Jump") && airborne == false)
        {
            playerRB.AddForce(Vector2.up * jumpSpeed);
            animator.SetBool("airborne", true);
            airborne = true;
        }
        Physics2D.IgnoreLayerCollision(8,11, playerRB.velocity.y > 0);
    }
    void FixedUpdate ()
    {
        
        float move = Input.GetAxis("Horizontal");
        
        playerRB.velocity = new Vector2(move * movementSpeed, playerRB.velocity.y);
        
        if(move != 0)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
        if(move < 0)
        {
            isFlipped = true;
            
        }
        else if (move > 0)
        {
            isFlipped = false;
            
        }
        Flip();

        
	}

    void Flip()
    {
        if(isFlipped == true)
        {
            rend.flipX = true;
        }
        else
        {
            rend.flipX = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool("airborne", false);
        airborne = false;
    }

    public int getHealth()
    {
        return health;
    }
    public void setHealth(int value)
    {
        health = value;
    }
}

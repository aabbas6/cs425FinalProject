
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpSpeed = 250f;
    public GameObject character;
    public GameObject attackCol;
    public GameObject hitParticle;
    public Collider2D attackTrigger;
    private Rigidbody2D playerRB;
    private Animator animator;
    private SpriteRenderer rend;
    private bool isFlipped;
    private bool isAttacking;
    private float attackTimer = 0.0f;
    private float attackCD = 0.3f;
    public bool isHit = false;
    private bool airborne;
    
    public int health = 100;

    public float invulnerableTimer = 0.0f;
    public bool isInvul = false;
    public float spriteBlinkingMiniDuration = 0.3f;
    public float spriteBlinkingTimer = 0.0f;

    private float timeTilGameOverScreen = 2f;
    // Use this for initialization
	void Start ()
    {
        playerRB = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        attackTrigger = attackCol.GetComponent<BoxCollider2D>();
        attackTrigger.enabled = false;
	}

    void Update()
    {
        
        if (health <= 0)
        {
            Dead();
        }
        else
        {
            if (isHit)
            {
                invulTime();
                Physics2D.IgnoreLayerCollision(8, 9,isInvul);
                Physics2D.IgnoreLayerCollision(8, 12, isInvul);
                
            }
            if (Input.GetButtonDown("Jump") && airborne == false)
            {
                playerRB.AddForce(Vector2.up * jumpSpeed);
                animator.SetBool("airborne", true);
                airborne = true;
            }
            Physics2D.IgnoreLayerCollision(8, 11, playerRB.velocity.y > 0);

        }
        if (isAttacking)
        {
            if (attackTimer > 0)
                attackTimer -= Time.deltaTime;
            else
            {
                animator.SetBool("attacking", false);
                isAttacking = false;
                attackTrigger.enabled = false;
            }
        }
    }
    void FixedUpdate ()
    {
        if (health > 0)
        {
            float move = Input.GetAxis("Horizontal");

            playerRB.velocity = new Vector2(move * movementSpeed, playerRB.velocity.y);

            if (move != 0)
            {
                animator.SetBool("isWalk", true);
            }
            else
            {
                animator.SetBool("isWalk", false);
            }
            if (move < 0)
            {
                isFlipped = true;

            }
            else if (move > 0)
            {
                isFlipped = false;
            }
            Flip();

            if(Input.GetMouseButtonDown(0) && !isAttacking)
            {
                animator.SetBool("attacking", true);
                isAttacking = true;
                attackTimer = attackCD;
                attackTrigger.enabled = true;
            }
        }
	}
    void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool("airborne", false);
        airborne = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking == true && collision.gameObject.tag == "enemy")
        {
            Instantiate(hitParticle, attackCol.transform.position, Quaternion.identity);
            int enemyHealth = collision.GetComponent<EnemyAI>().getHealth();
            enemyHealth -= 10;
            collision.GetComponent<EnemyAI>().setHealth(enemyHealth);
        }
    }


    public bool isInvulnerable()
    {
        return isInvul;
    }
    public void setHit(bool hit)
    {
        isHit = hit;
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
    void invulTime()
    {
        invulnerableTimer += Time.deltaTime;
        isInvul = true;
        SpriteBlink();
        if(invulnerableTimer > 3f)
        {
            invulnerableTimer = 0.0f;
            isHit = false;
            rend.enabled = true;
            isInvul = false;
        }
    }

    void SpriteBlink()
    {
        spriteBlinkingTimer += Time.deltaTime;
        //Debug.Log("ya suppose to blink foo");
        if(spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (rend.enabled == true)
                rend.enabled = false;
            else
                rend.enabled = true;
        }
    }
    
    public int getHealth()
    {
        return health;
    }
    public void setHealth(int value)
    {
        health = value;
    }

    void Dead()
    {
        animator.SetBool("isDead", true);
        Destroy(gameObject, 2f);
    }
}
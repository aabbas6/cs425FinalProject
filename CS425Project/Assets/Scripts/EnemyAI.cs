
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyAI : MonoBehaviour {

    public GameObject player;
    public GameObject detection;
    public GameObject fireBall;
    public GameObject firePoint;
    Animator anim;
    private Rigidbody2D enemyRB;
    private SpriteRenderer enemyRend;

    public int health = 500;
    public bool dead = false;
    public float deathTimer = 1f;
    public GameObject getPlayer()
    {
        return player;
    }

    public Rigidbody2D getRB()
    {
        return enemyRB;
    }

    public GameObject getDetection()
    {
        return detection;
    }

    public SpriteRenderer getRenderer()
    {
        return enemyRend;
    }
	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
        detection = GameObject.Find("wallDetection");
        enemyRend = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        Physics2D.IgnoreLayerCollision(9, 11);
        Physics2D.IgnoreLayerCollision(12, 10);
        Physics2D.IgnoreLayerCollision(12, 11);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("distance", Vector2.Distance(transform.position, player.transform.position));
        anim.SetFloat("preferredDistance", Vector2.Distance(transform.position, player.transform.position));
        if (health <= 0)
        {
            //Debug.Log("You killed it mate");
            DeadMate();
            
        }
    }

    public void DeadMate()
    {
        anim.SetBool("Dead", true);
        deathTimer -= Time.deltaTime;
        if (deathTimer < 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }

    public void Attack1()
    {
        anim.SetInteger("Action", 0);
        if(Mathf.Abs(player.transform.position.x - transform.position.x) < 3)
        {
            if (!player.GetComponent<CharacterMovement>().isInvulnerable())
            {
                int phealth = player.GetComponent<CharacterMovement>().getHealth();
                phealth -= 25;
                player.GetComponent<CharacterMovement>().setHealth(phealth);
                player.GetComponent<CharacterMovement>().setHit(true);
            }
        }
    }

    public void Attack2()
    {
        anim.SetInteger("Action", 0);
        GameObject fb = Instantiate(fireBall, firePoint.transform.position, transform.rotation);
        fb.GetComponent<Rigidbody2D>().AddForce(transform.forward * 10f);
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


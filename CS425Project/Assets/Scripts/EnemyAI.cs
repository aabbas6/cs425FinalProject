using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public GameObject player;
    public GameObject detection;
    public GameObject fireBall;
    public GameObject firePoint;
    Animator anim;
    private Rigidbody2D enemyRB;
    private SpriteRenderer enemyRend;

    public int health = 100;
    public bool dead = false;

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
        fireBall = GetComponent<GameObject>();
        firePoint = GetComponent<GameObject>();
        Physics2D.IgnoreLayerCollision(9, 11);
	}
	
	// Update is called once per frame
	void Update ()
    {
        anim.SetFloat("distance", Vector2.Distance(transform.position, player.transform.position));
        anim.SetFloat("preferredDistance", Vector2.Distance(transform.position, player.transform.position));
    }

    public void Attack1()
    {
        anim.SetInteger("Action", 0);
        if(Mathf.Abs(player.transform.position.x - transform.position.x) < 1)
        {
            int phealth = player.GetComponent<CharacterMovement>().getHealth();
            phealth -= 50;
            player.GetComponent<CharacterMovement>().setHealth(phealth);
        }
    }

    public void Attack2()
    {
        anim.SetInteger("Action", 0);
        GameObject fb = Instantiate(fireBall, firePoint.transform.position, transform.rotation);
        fb.GetComponent<Rigidbody2D>().AddForce(transform.forward * 10f);
    }
    
 

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

    public float projectileSpeed;
    private Animator animator;
    private SpriteRenderer rend;
    private Rigidbody2D prb;
    public GameObject explosion;
    
    public void Start()
    {
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        Physics2D.IgnoreLayerCollision(12, 10);
        Physics2D.IgnoreLayerCollision(12, 11);
    }
    
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<CharacterMovement>().isInvulnerable())
            {
                int health = collision.gameObject.GetComponent<CharacterMovement>().getHealth();
                health -= 10;
                collision.gameObject.GetComponent<CharacterMovement>().setHealth(health);
                collision.gameObject.GetComponent<CharacterMovement>().setHit(true);
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

    }
}

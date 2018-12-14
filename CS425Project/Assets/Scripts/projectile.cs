using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

    public float projectileSpeed;
    private Animator animator;
    private SpriteRenderer rend;
    private Rigidbody2D prb;
    private void Awake()
    {
        Destroy(gameObject, 4f);
    }
    public void Start()
    {
        animator = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            animator.SetBool("hit", true);
            Destroy(gameObject, 1f);
        }
    }
}

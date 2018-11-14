using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float movementSpeed;
    public float jumpSpeed;
    public GameObject character;
    private Rigidbody2D playerRB;

    private SpriteRenderer rend;
	// Use this for initialization
	void Start ()
    {
        playerRB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        float move = Input.GetAxis("Horizontal");

       
        playerRB.velocity = new Vector2(move * movementSpeed, playerRB.velocity.y);
	}
}

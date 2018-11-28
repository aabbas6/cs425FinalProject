using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    private EnemyStats enemyStat;
    private Rigidbody2D enemyRB2D;
    private Animator animator;
    private bool isFlipped;
    private bool inCombat;
    public GameObject character;
    private Rigidbody2D playerRB2D;

    public int health;
    public float movementSpeed;
    public float jumpSpeed;
    public int defense;
    public int attack;

    public string action;
    // Use this for initialization

    void Start ()
    {
        enemyStat = GetComponent<EnemyStats>();
        enemyRB2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        character = GetComponent<GameObject>();
        playerRB2D = character.GetComponent<Rigidbody2D>();
        inCombat = false;
        health = enemyStat.getHealth();
        movementSpeed = enemyStat.getMovementSpeed();
        jumpSpeed = enemyStat.getJumpSpeed();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Mathf.Abs(enemyRB2D.transform.localPosition.x - playerRB2D.transform.localPosition.x) < 4)
        {
            inCombat = true;
        }


    }

    void FixedUpdate ()
    {
        
	}
}

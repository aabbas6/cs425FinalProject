using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseFSM : StateMachineBehaviour {

    // Use this for initialization
    public GameObject enemy;
    public GameObject opponent;
    
    public float movementSpeed = 5000.0f;
    public float jumpSpeed = 200.0f;
    public Rigidbody2D enemyRB;
    public GameObject wallDetection;
    public SpriteRenderer enemyRend;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        enemyRB = enemy.GetComponent<EnemyAI>().getRB();
        enemyRend = enemy.GetComponent<EnemyAI>().getRenderer();
        opponent = enemy.GetComponent<EnemyAI>().getPlayer();
        wallDetection = enemy.GetComponent<EnemyAI>().getDetection();
    }

}

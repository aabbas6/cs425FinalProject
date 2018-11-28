using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseFSM : StateMachineBehaviour {

    // Use this for initialization
    public GameObject enemy;
    public GameObject opponent;
    public float movementSpeed = 2.0f;
    public float jumpSpeed = 1.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        opponent = enemy.GetComponent<EnemyAI>().getPlayer();
    }

}

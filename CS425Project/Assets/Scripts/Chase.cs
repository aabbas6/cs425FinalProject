using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : EnemyBaseFSM {

    bool isFlipped = true; //check the sprite's direction
    bool isAirborne = false;
    Vector3 lastPosition;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetDirection();
        
        if(opponent.transform.position.y > enemy.transform.position.y + 1 && isAirborne == false)
        {
            Debug.Log("I am trying to jump, but i am too heavy.");
            enemyRB.AddForce(Vector2.up * 20000f);
            isAirborne = true;
        }
        else
        {
            if (isFlipped)
                enemyRB.velocity = new Vector2(-movementSpeed * .5f, enemyRB.velocity.y);
            else
                enemyRB.velocity = new Vector2(movementSpeed * .5f, enemyRB.velocity.y);
            isAirborne = false;
        }
    }

    public void GetDirection()
    {
        if(opponent.transform.position.x < enemy.transform.position.x)
        {
            isFlipped = true;
           
            enemy.transform.eulerAngles = new Vector3(0, -180, 0);

        }
        else if(opponent.transform.position.x > enemy.transform.position.x)
        {
            isFlipped = false;
            
            enemy.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    
	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}

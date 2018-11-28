using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    public int health;
    public float movementSpeed;
    public float jumpSpeed;
    public int defense;
    public int attack;
    public bool isAlive;

	// Use this for initialization
	public int getHealth()
    {
        return health;
    }

    public float getMovementSpeed()
    {
        return movementSpeed;
    }

    public int getDefense()
    {
        return defense;
    }

    public int getAttack()
    {
        return attack;
    }

    public bool getIsAlive()
    {
        return isAlive;
    }

    public float getJumpSpeed()
    {
        return jumpSpeed; ;
    }
}

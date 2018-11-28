using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public GameObject player;

    Animator anim;

    public GameObject getPlayer()
    {
        return player;
    }
	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        anim.SetFloat("distance", Vector2.Distance(transform.position, player.transform.position));
	}
}

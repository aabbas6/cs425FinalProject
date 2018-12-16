using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBehavior : MonoBehaviour {
    public GameObject player;

	// Use this for initialization
	void Start ()
    {
        player = GetComponent<GameObject>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        Physics2D.IgnoreLayerCollision(8, 11, (player.GetComponent<Rigidbody2D>().velocity.y > 0f));
	}
}

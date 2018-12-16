using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    private GameObject player;
    private Text text;
    private int health;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        text = GetComponent<Text>();
        text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
    }
	
	// Update is called once per frame
	void Update ()
    {
        health = player.GetComponent<CharacterMovement>().getHealth();
        text.text = "Health: " + health;

    }
}

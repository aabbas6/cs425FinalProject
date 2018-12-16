using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class playerDetection : MonoBehaviour {

    public GameObject player;
    public GameObject boss;
    // Use this for initialization

    private void Start()
    {
        
    }
    // Update is called once per frame
    public void FixedUpdate ()
    {
	    if(player == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
	}
}

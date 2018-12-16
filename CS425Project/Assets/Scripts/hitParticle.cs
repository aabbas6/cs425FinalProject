using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitParticle : MonoBehaviour {

    // Use this for initialization
    public void Awake()
    {
        Destroy(gameObject, .3f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionTimer : MonoBehaviour {

    // Use this for initialization
    private void Awake()
    {
        Destroy(gameObject, 1f);
    }
}

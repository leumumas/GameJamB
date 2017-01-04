using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    private int hearts,
                difficulty,
                speed;
    private float movex = 0f;

    // Use this for initialization
    void Start () {
        hearts = 3;
        difficulty = 0;
        speed = 50;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
            movex = -1;
        else if (Input.GetKey(KeyCode.D))
            movex = 1;
        else
            movex = 0;
        Debug.Log(movex);
    }
}

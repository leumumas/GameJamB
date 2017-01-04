using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    //Attributes Character
    private int hearts,
                difficulty,
                speed,
                playerNumber;
    //Movements
    private float moveX = 0f,
                  moveY = 0f;

    void Start () {
        hearts = 3;
        difficulty = 0;
        speed = 50;
	}
	
	void Update () {

        // Player 1 moving left or right
        if (Input.GetKey(KeyCode.A))
            moveX = -1;
        else if (Input.GetKey(KeyCode.D))
            moveX = 1;
        else
            moveX = 0;
        
        //Player 1 moving up or down
        if (Input.GetKey(KeyCode.W))
            moveY = 1;
        else if (Input.GetKey(KeyCode.S))
            moveY = -1;
        else
            moveY = 0;
        Debug.Log(moveY);
    }
}

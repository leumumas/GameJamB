using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public int playerNumberB;
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

        switch (playerNumberB)
        {
            case 0:
                // Player 1 moving left or right
                if (Input.GetKey(KeyCode.A))
                    moveX = -1;
                else if (Input.GetKey(KeyCode.D))
                    moveX = 1;
                else
                    moveX = 0;
                Debug.Log("1X"+moveX);

                //Player 1 moving up or down
                if (Input.GetKey(KeyCode.W))
                    moveY = 1;
                else if (Input.GetKey(KeyCode.S))
                    moveY = -1;
                else
                    moveY = 0;
                Debug.Log("1Y"+moveY);
                break;
            case 1:
                // Player 2 moving left or right
                if (Input.GetKey("left"))
                    moveX = -1;
                else if (Input.GetKey("right"))
                    moveX = 1;
                else
                    moveX = 0;
                Debug.Log("2X"+moveX);

                //Player 1 moving up or down
                if (Input.GetKey("up"))
                    moveY = 1;
                else if (Input.GetKey("down"))
                    moveY = -1;
                else
                    moveY = 0;
                Debug.Log("2Y"+moveY);
                break;
            default:
                Debug.Log("What the fuck");
                break;
        }
    }
}

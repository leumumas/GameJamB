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
    private float moveY = 0f, moveX = 0f,
                reactionTime;
    //Triggers
    public bool door, item, inside;
    public GameObject crItem;
    Animator anim;

    void Start () {
        anim = GetComponent<Animator>();
        hearts = 3;
        difficulty = 15;
        speed = 8;
        door = false;
        reactionTime = 5f;
	}

	void Update () {

        switch (playerNumberB)
        {
            case 0:
                // Player 1 moving left or right
                if (Input.GetKey(KeyCode.A))
                {
                    transform.position += Vector3.left * speed * Time.deltaTime;
                    moveX = -1.0f;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    transform.position += Vector3.right * speed * Time.deltaTime;
                    moveX = 1.0f;
                }
                else
                    moveX = 0f;


                //Player 1 moving up or down
                if (Input.GetKey(KeyCode.W) && door)
                {
                    moveY = 1;
                }
                else if (Input.GetKey(KeyCode.S) && item)
                {
                    Debug.Log(crItem.name);
                    Destroy(crItem);
                    crItem = null;
                    moveY = 1;
                }
                else
                moveY = 0;
                Debug.Log("1Y"+moveY);
                break;
            case 1:
                // Player 2 moving left or right
                if (Input.GetKey("left"))
                {
                    transform.position += Vector3.left * speed * Time.deltaTime;
                    moveX = -1.0f;
                }
                else if (Input.GetKey("right"))
                {
                    transform.position += Vector3.right * speed * Time.deltaTime;
                    moveX = 1.0f;
                }
                else
                    moveX = 0f;

                //Player 2 moving up or down
                if (Input.GetKey("up") && door)
                {
                    moveY = 1;
                }
                else if (Input.GetKey("down") && item)
                {
                    Destroy(crItem);
                    crItem = null;
                    moveY = 1;
                }
                else
                    moveY = 0;
                Debug.Log("2Y"+moveY);
                break;
            default:
                Debug.Log("What the fuck");
                break;
        }
        anim.SetFloat("Moving", moveX);
        Debug.Log(playerNumberB + "" + door);
    }
}

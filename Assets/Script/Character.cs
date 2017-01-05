using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public int playerNumberB;
    //Attributes Character
    public int hearts,
                difficulty,
                speed,
                wins,
                keyLeft;
    //Movements
    private float moveY = 0f, moveX = 0f;
    public float reactionTime;
    //Triggers
    public bool door, item, outside, shield, isPhase1;
    public GameObject crItem, triggerPop;
    Animator anim;
    public Sprite Robot;
    public RuntimeAnimatorController robotAnim;
    public int itemsLeft;
    public int nbBonus, nbMalus;

    private KeyCode currentKey;

    void Start () {
        itemsLeft = 6;
        anim = GetComponent<Animator>();
        hearts = 3;
        difficulty = 15;
        speed = 5;
        door = false;
        item = false;
        reactionTime = 5f;
        shield = false;
        if (playerNumberB == 1)
        {
            GetComponentInParent<SpriteRenderer>().sprite = Robot;
            anim.runtimeAnimatorController = robotAnim;
        }
        nbBonus = 0;
        nbMalus = 0;
        isPhase1 = true;
    }

	void Update () {
        if (isPhase1)
            phase1();
        if (!isPhase1)
            phase2();
    }

    public void updateStats (Items it)
    {
        //Debug.Log(crItem.name);
        if (it.isBonus)
        {
            if (nbBonus < 3)
            {
                difficulty += it.promptItem;
                reactionTime += it.reactionItem;
                if (it.promptItem == 0 && it.reactionItem == 0)
                    shield = true;
                Destroy(crItem);
                nbBonus += 1;
                itemsLeft -= 1;
                crItem = null;
                item = false;
            }
        }
        else
        {
            if (nbMalus < 3)
            {
                 GameObject.Find("ManagerObject").GetComponent<GameManager>().malusUpdate(it, playerNumberB);
                Destroy(crItem);
                nbMalus += 1;
                itemsLeft -= 1;
                crItem = null;
                item = false;
            }
                
        }
    }

    void phase1()
    {
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
                   outside = false;
                     GameObject.Find("ManagerObject").GetComponent<GameManager>().setTownVisibility(playerNumberB, outside);
                    moveY = 1;
                    if (outside == false)
                    {

                    }
                }
                else if (Input.GetKey(KeyCode.S) && item)
                {
                    if (itemsLeft > 0)
                    {
                        if (!outside)
                            updateStats(crItem.GetComponent<Items>());
                    }
                    moveY = 1;
                }
                else if (Input.GetKey(KeyCode.S) && door)
                {
                    outside = true;
                     GameObject.Find("ManagerObject").GetComponent<GameManager>().setTownVisibility(playerNumberB, outside);
                    moveY = 1;
                }
                else
                    moveY = 0;
                Debug.Log("1Y" + moveY);
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
                    outside = false;
                     GameObject.Find("ManagerObject").GetComponent<GameManager>().setTownVisibility(playerNumberB, outside);
                    moveY = 1;
                }
                else if (Input.GetKey("down") && item)
                {
                    if (itemsLeft > 0)
                    {
                        if (!outside)
                            updateStats(crItem.GetComponent<Items>());
                    }
                    moveY = -1;
                }
                else if (Input.GetKey("down") && door)
                {
                    outside = true;
                     GameObject.Find("ManagerObject").GetComponent<GameManager>().setTownVisibility(playerNumberB, outside);
                    moveY = -1;
                }
                else
                    moveY = 0;
                Debug.Log("2Y" + moveY);
                break;
            default:
                Debug.Log("What the fuck");
                break;
        }
        if (door)
            triggerPop.SetActive(true);
        else
            triggerPop.SetActive(false);
        anim.SetFloat("Moving", moveX);
        Debug.Log(playerNumberB + "" + door);
    }

    void phase2()
    {
        switch (playerNumberB)
        {
            case 0:
                    while (keyLeft != 0)
                    {
                        currentKey = generateNewKeyPlayer1();
                        if (Input.GetKey(KeyCode.W))
                            if (currentKey == KeyCode.W)
                                goodKeyPressed();
                            else
                                badKeyPressed();
                        else if (Input.GetKey(KeyCode.A))
                            if (currentKey == KeyCode.A)
                                goodKeyPressed();
                            else
                                badKeyPressed();
                        else if (Input.GetKey(KeyCode.S))
                            if (currentKey == KeyCode.S)
                                goodKeyPressed();
                            else
                                badKeyPressed();
                        else if (Input.GetKey(KeyCode.D))
                            if (currentKey == KeyCode.D)
                                goodKeyPressed();
                            else
                                badKeyPressed();
                    }
            break;
            case 1:
                while (keyLeft != 0)
                {
                    currentKey = generateNewKeyPlayer2();
                    if (Input.GetKey(KeyCode.UpArrow))
                        if (currentKey == KeyCode.UpArrow)
                            goodKeyPressed();
                        else
                            badKeyPressed();
                    else if (Input.GetKey(KeyCode.LeftArrow))
                        if (currentKey == KeyCode.LeftArrow)
                            goodKeyPressed();
                        else
                            badKeyPressed();
                    else if (Input.GetKey(KeyCode.DownArrow))
                        if (currentKey == KeyCode.DownArrow)
                            goodKeyPressed();
                        else
                            badKeyPressed();
                    else if (Input.GetKey(KeyCode.RightArrow))
                        if (currentKey == KeyCode.RightArrow)
                            goodKeyPressed();
                        else
                            badKeyPressed();
                }
            break;
        }
    }

    KeyCode generateNewKeyPlayer1()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                return KeyCode.W;
            case 1:
                return KeyCode.A;
            case 2:
                return KeyCode.S;
            case 3:
                return KeyCode.D;
            default: return KeyCode.W;
        }
    }

    KeyCode generateNewKeyPlayer2()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                return KeyCode.UpArrow;
            case 1:
                return KeyCode.LeftArrow;
            case 2:
                return KeyCode.DownArrow;
            case 3:
                return KeyCode.RightArrow;
            default: return KeyCode.UpArrow;
        }
    }

    void goodKeyPressed()
    {
        keyLeft--;
    }

    void badKeyPressed()
    {
        if (shield)
            shield = false;
        else
            hearts--;
    }

    public void characterUpdate()
    {
        itemsLeft = 6;
        anim = GetComponent<Animator>();
        hearts = 3;
        difficulty = 15;
        speed = 5;
        door = false;
        item = false;
        reactionTime = 5f;
        shield = false;
        if (playerNumberB == 1)
        {
            GetComponentInParent<SpriteRenderer>().sprite = Robot;
            anim.runtimeAnimatorController = robotAnim;
        }
        nbBonus = 0;
        nbMalus = 0;
        isPhase1 = true;
    }
}
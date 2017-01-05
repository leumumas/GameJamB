﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public int playerNumberB;
    //Attributes Character
    public int hearts,
                difficulty,
                speed,
                wins;
    //Movements
    private float moveY = 0f, moveX = 0f;
    public float reactionTime;
    //Triggers
    public bool door, item, outside, shield;
    public GameObject crItem, triggerPop;
    Animator anim;
    public Sprite Robot;
    public RuntimeAnimatorController robotAnim;
    public int itemsLeft;
    public int nbBonus, nbMalus;

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
                    outside = false;
                    GameManager.instance.setTownVisibility(playerNumberB, outside);
                    moveY = 1;
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
                    GameManager.instance.setTownVisibility(playerNumberB, outside);
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
                    outside = false;
                    GameManager.instance.setTownVisibility(playerNumberB, outside);
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
                    GameManager.instance.setTownVisibility(playerNumberB, outside);
                    moveY = -1;
                }
                else
                    moveY = 0;
                Debug.Log("2Y"+moveY);
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
                GameManager.instance.malusUpdate(it, playerNumberB);
                Destroy(crItem);
                nbMalus += 1;
                itemsLeft -= 1;
                crItem = null;
                item = false;
            }
                
        }
    }
}
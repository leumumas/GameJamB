using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdventure : MonoBehaviour
{
    public int playerNumberB;
    //Attributes Character
    public int hearts,
    difficulty,
    speed,
    wins,
    keyLeft;
    int startDifficulty = 10;
    //Movements
    private float moveY = 0f, moveX = 0f;
    public float reactionTime;
    //Triggers
    public bool door, item, outside, shield, isPhase1, press, useRune, street, intersection, grounded;
    public GameObject crItem, triggerPop;
    Animator anim;
    public Sprite Robot;
    public RuntimeAnimatorController robotAnim;
    public int itemsLeft, nbBonus, nbMalus;
    int layerP1; //10111111111 1535  1011111111 767  10111111 191
    int layerP2; //11011111111 1791  1101111111 895  11011111 223
    GameManager GM;
    GameManagerAdventure GMA;
    public int runeComplete, heartsLost, crStreet;
    public float jumpValue, angleMove, speedRotation;
    Vector3 positionMove;
    Rigidbody playerBody;
    public DoorTrigger crDoor;
    public Intersection crIntersection;
    private float startTime;
    private float journeyLength;
    bool intersectionMove;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        try
        {
            GM = GameObject.Find("ManagerObject").GetComponent<GameManager>();
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("No GameManager");
        }
        try
        {
            GMA = GameObject.Find("ManagerObject").GetComponent<GameManagerAdventure>();
        }
        catch (NullReferenceException ex2)
        {
            Debug.Log("No GameManagerAdventure");
        }
        playerBody = gameObject.GetComponent<Rigidbody>();
        useRune = false;
        itemsLeft = 6;
        anim = GetComponent<Animator>();
        hearts = 3;
        difficulty = startDifficulty;
        speed = 5;
        door = false;
        item = false;
        reactionTime = 5f;
        shield = false;
        outside = true;
        press = false;
        angleMove = gameObject.transform.rotation.y;
        intersectionMove = false;
        if (playerNumberB == 1)
        {
            layerP2 = binaryConversion("111011111111");
            GetComponentInParent<SpriteRenderer>().sprite = Robot;
            anim.runtimeAnimatorController = robotAnim;
            GetComponentInChildren<Camera>().cullingMask = layerP2;
            this.gameObject.layer = 11;
        }
        else
        {
            layerP1 = binaryConversion("110111111111");
            GetComponentInChildren<Camera>().cullingMask = layerP1;
        }
        nbBonus = 0;
        nbMalus = 0;
        isPhase1 = true;
    }

    public void setCullingMask(string mask)
    {
        if (playerNumberB == 1)
        {
            layerP2 = binaryConversion(mask);
            GetComponentInChildren<Camera>().cullingMask = layerP2;
        }
        else
        {
            layerP1 = binaryConversion(mask);
            GetComponentInChildren<Camera>().cullingMask = layerP1;
        }
    }

    int binaryConversion(string mask)
    {
        string myStringBinaryNumber = mask;
        return Convert.ToInt32(myStringBinaryNumber, 2);    // From (string)binary to int
    }

    public void loseLife(int dmg)
    {
        hearts = hearts - dmg;
    }

    public void startRotation()
    {
        StartCoroutine("MoveObject");
        /*Vector3 crAngle = transform.eulerAngles;
        startTime = Time.time;
        journeyLength = Vector3.Distance(crAngle, new Vector3(0, angleMove, 0));*/
    }

    public void setPositionMove(Vector3 pos)
    {
        positionMove = pos;
    }

    IEnumerator MoveObject()
    {
        intersectionMove = true;
        float startTime = Time.time;
        Vector3 crAngle = transform.eulerAngles;
        Vector3 crPosition = transform.position;
        while (Time.time < startTime + speedRotation)
        {
            transform.eulerAngles = Vector3.Lerp(crAngle, new Vector3(0, angleMove, 0), (Time.time - startTime) / speedRotation);
            transform.position = Vector3.Lerp(crPosition, positionMove, (Time.time - startTime) / speedRotation);
            yield return null;
        }
        transform.eulerAngles = new Vector3(0, angleMove, 0);
        intersectionMove = false;
    }

    void Update()
    {
        /*if (gameObject.transform.eulerAngles.y != angleMove)
        {
            float distCovered = (Time.time - startTime) * speedRotation;
            float fracJourney = distCovered / journeyLength;
            Vector3 crAngle = transform.eulerAngles;
            transform.eulerAngles = Vector3.Lerp(crAngle, new Vector3(0, angleMove, 0), fracJourney);
            Debug.Log(fracJourney);
        }*/
        if (!intersectionMove)
        {
            if (GM != null)
            {
                switch (GM.CrPhase)
                {
                    case 0:
                        phase1();
                        break;
                    case 1:
                        phase2();
                        break;
                }
            }
            else
                adventureControl();
        }
    }

    void adventureControl()
    {
        switch (playerNumberB)
        {
            case 0:
                // Player 1 moving left or right
                moveX = Input.GetAxis("Horizontal");
                if (moveX <= 0.1 && moveX >= -0.1)
                    moveX = 0;
                else
                    transform.position += moveX * (Quaternion.Euler(transform.eulerAngles) * Vector3.right) * speed * Time.deltaTime;
                //Player 1 moving up or down
                if ((Input.GetButtonDown("Interact")) && door)
                {
                    if (outside)
                    {
                        outside = false;
                        crDoor.houseVisibility(outside);
                    }
                    else
                    {
                        outside = true;
                        crDoor.houseVisibility(outside);
                    }
                }
                else if ((Input.GetButtonDown("Interact")) && item)
                {
                    if (itemsLeft > 0)
                    {
                        if (!outside)
                            updateStats(crItem.GetComponent<Items>());
                    }
                }
                else if ((Input.GetButtonDown("Interact")) && intersection && outside)
                {
                    crIntersection.switchStreet(crStreet);
                }
                //Debug.Log("Velocity :" + playerBody.velocity.y);
                if ((Input.GetButtonDown("Jump")) && grounded)
                {
                    grounded = false;
                    playerBody.AddForce(new Vector3(0, jumpValue, 0), ForceMode.Impulse);
                }
                break;
            case 1:
                // Player 2 moving left or right
                moveX = Input.GetAxis("Horizontal2");
                if (moveX <= 0.1 && moveX >= -0.1)
                    moveX = 0;
                else
                    transform.position += moveX * Vector3.right * speed * Time.deltaTime;

                //Player 2 moving up or down
                if ((Input.GetButtonDown("Interact2")) && door)
                {
                    if (outside)
                    {
                        outside = false;
                        crDoor.houseVisibility(outside);
                    }
                    else
                    {
                        outside = true;
                        crDoor.houseVisibility(outside);
                    }
                }
                else if ((Input.GetButtonDown("Interact2")) && item)
                {
                    if (itemsLeft > 0)
                    {
                        if (!outside)
                            updateStats(crItem.GetComponent<Items>());
                    }
                    moveY = -1;
                }
                else
                    moveY = 0;
                break;
            default:
                break;
        }
        if (door || (intersection && outside))
            triggerPop.SetActive(true);
        else
            triggerPop.SetActive(false);
        anim.SetFloat("Moving", moveX);
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }

    public void updateStats(Items it)
    {
        int nbItem = crItem.GetComponent<Items>().itemNb;
        Items toDelete;
        //Debug.Log(crItem.name);
        if (it.isBonus)
        {
            if (nbBonus < 3)
            {
                difficulty += it.promptItem;
                reactionTime += it.reactionItem;
                if (it.promptItem == 0 && it.reactionItem == 0)
                    shield = true;
                for (int i = 0; i < GM.spawnedBonusP1.Count; i++)
                {
                    if (GM.spawnedBonusP1[i].itemNb == nbItem)
                    {
                        toDelete = GM.spawnedBonusP1[i];
                        GM.spawnedBonusP1.Remove(toDelete);
                        Destroy(toDelete.gameObject);
                    }
                    if (GM.spawnedBonusP2[i].itemNb == nbItem)
                    {
                        toDelete = GM.spawnedBonusP2[i];
                        GM.spawnedBonusP2.Remove(toDelete);
                        Destroy(toDelete.gameObject);
                    }
                }
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
                for (int i = 0; i < GM.spawnedBonusP1.Count; i++)
                {
                    if (GM.spawnedBonusP1[i].itemNb == nbItem)
                    {
                        toDelete = GM.spawnedBonusP1[i];
                        GM.spawnedBonusP1.Remove(toDelete);
                        Destroy(toDelete.gameObject);
                    }
                    if (GM.spawnedBonusP2[i].itemNb == nbItem)
                    {
                        toDelete = GM.spawnedBonusP2[i];
                        GM.spawnedBonusP2.Remove(toDelete);
                        Destroy(toDelete.gameObject);
                    }
                }
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
                moveX = Input.GetAxis("Horizontal");
                if (moveX <= 0.1 && moveX >= -0.1)
                    moveX = 0;
                else
                    transform.position += moveX * Vector3.right * speed * Time.deltaTime;
                //Player 1 moving up or down
                if ((Input.GetButtonDown("Enter")) && door)
                {
                    if (outside)
                    {
                        outside = false;
                        GM.setTownVisibility(playerNumberB, outside);
                        moveY = 1;
                    }
                    else
                    {
                        outside = true;
                        GM.setTownVisibility(playerNumberB, outside);
                        moveY = 1;
                    }
                }
                else if ((Input.GetButtonDown("Get")) && item)
                {
                    if (itemsLeft > 0)
                    {
                        if (!outside)
                            updateStats(crItem.GetComponent<Items>());
                    }
                    moveY = 1;
                }
                else
                    moveY = 0;
                break;
            case 1:
                // Player 2 moving left or right
                moveX = Input.GetAxis("Horizontal2");
                if (moveX <= 0.1 && moveX >= -0.1)
                    moveX = 0;
                else
                    transform.position += moveX * Vector3.right * speed * Time.deltaTime;

                //Player 2 moving up or down
                if ((Input.GetButtonDown("Enter2")) && door)
                {
                    if (outside)
                    {
                        outside = false;
                        GameObject.Find("ManagerObject").GetComponent<GameManager>().setTownVisibility(playerNumberB, outside);
                        moveY = 1;
                    }
                    else
                    {
                        outside = true;
                        GameObject.Find("ManagerObject").GetComponent<GameManager>().setTownVisibility(playerNumberB, outside);
                        moveY = 1;
                    }
                }
                else if ((Input.GetButtonDown("Get2")) && item)
                {
                    if (itemsLeft > 0)
                    {
                        if (!outside)
                            updateStats(crItem.GetComponent<Items>());
                    }
                    moveY = -1;
                }
                else
                    moveY = 0;
                break;
            default:
                break;
        }
        if (door)
            triggerPop.SetActive(true);
        else
            triggerPop.SetActive(false);
        anim.SetFloat("Moving", moveX);
    }

    public void characterUpdate()
    {
        itemsLeft = 6;
        anim = GetComponent<Animator>();
        hearts = 3;
        difficulty = startDifficulty;
        speed = 5;
        door = false;
        item = false;
        reactionTime = 5f;
        shield = false;
        outside = true;
        press = false;
        if (playerNumberB == 1)
        {
            GetComponentInParent<SpriteRenderer>().sprite = Robot;
            anim.runtimeAnimatorController = robotAnim;
        }
        nbBonus = 0;
        nbMalus = 0;
        isPhase1 = true;
    }

    void phase2()
    {
        if (useRune && hearts > 0)
        {
            switch (playerNumberB)
            {
                case 0:
                    if (Input.GetKeyDown(KeyCode.W))
                        GM.runeBattle.checkRune(playerNumberB, 2, false);
                    if (Input.GetKeyDown(KeyCode.A))
                        GM.runeBattle.checkRune(playerNumberB, 3, false);
                    if (Input.GetKeyDown(KeyCode.S))
                        GM.runeBattle.checkRune(playerNumberB, 0, false);
                    if (Input.GetKeyDown(KeyCode.D))
                        GM.runeBattle.checkRune(playerNumberB, 1, false);
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                        GM.runeBattle.checkRune(playerNumberB, 2, false);
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                        GM.runeBattle.checkRune(playerNumberB, 3, false);
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                        GM.runeBattle.checkRune(playerNumberB, 0, false);
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                        GM.runeBattle.checkRune(playerNumberB, 1, false);
                    break;
            }
        }
    }

    public float getReaction()
    {
        return reactionTime / GM.reactionDifficulty;
    }
}
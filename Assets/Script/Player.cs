using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
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
	public bool door, item, outside, shield, isPhase1, press, useRune;
	public GameObject crItem, triggerPop;
	Animator anim;
	public Sprite Robot;
	public RuntimeAnimatorController robotAnim;
	public int itemsLeft;
	public int nbBonus, nbMalus;
	int layerP1; //10111111111 1535  1011111111 767  10111111 191
	int layerP2; //11011111111 1791  1101111111 895  11011111 223
	GameManager GM;
	public int runeComplete;
	public int heartsLost;

	private KeyCode currentKey;

	void Start () {
		DontDestroyOnLoad (gameObject);
		GM = GameObject.Find ("ManagerObject").GetComponent<GameManager> ();
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
		if (playerNumberB == 1) {
			layerP2 = binaryConversion ("111011111111");
			GetComponentInParent<SpriteRenderer> ().sprite = Robot;
			anim.runtimeAnimatorController = robotAnim;
			GetComponentInChildren<Camera> ().cullingMask = layerP2;
			this.gameObject.layer = 11;
		} 
		else {
			layerP1 = binaryConversion ("110111111111");
			GetComponentInChildren<Camera> ().cullingMask = layerP1;
		}
		nbBonus = 0;
		nbMalus = 0;
		isPhase1 = true;
	}

	public void setCullingMask(string mask) {
		if (playerNumberB == 1) {
			layerP2 = binaryConversion (mask);
			GetComponentInChildren<Camera> ().cullingMask = layerP2;
		} 
		else {
			layerP1 = binaryConversion (mask);
			GetComponentInChildren<Camera> ().cullingMask = layerP1;
		}
	}

	int binaryConversion(string mask) {
		string myStringBinaryNumber = mask;
		return Convert.ToInt32 (myStringBinaryNumber, 2);    // From (string)binary to int
	}

	public void loseLife(int dmg) {
		hearts = hearts - dmg;
	}

	void Update () {
		switch (GM.CrPhase) {
		case 0: 
			phase1 ();
			break;
		case 1: 
			phase2();
			break;
		}
	}

	public void updateStats (Items it)
	{
		int nbItem = crItem.GetComponent<Items> ().itemNb;
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
				for (int i = 0; i <GM.spawnedBonusP1.Count; i++) {
					if (GM.spawnedBonusP1 [i].itemNb == nbItem) {
						toDelete =GM.spawnedBonusP1[i];
						GM.spawnedBonusP1.Remove (toDelete);
						Destroy (toDelete.gameObject);
					}
					if (GM.spawnedBonusP2 [i].itemNb == nbItem) {
						toDelete =GM.spawnedBonusP2[i];
						GM.spawnedBonusP2.Remove (toDelete);
						Destroy (toDelete.gameObject);
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
				for (int i = 0; i <GM.spawnedBonusP1.Count; i++) {
					if (GM.spawnedBonusP1 [i].itemNb == nbItem) {
						toDelete =GM.spawnedBonusP1[i];
						GM.spawnedBonusP1.Remove (toDelete);
						Destroy (toDelete.gameObject);
					}
					if (GM.spawnedBonusP2 [i].itemNb == nbItem) {
						toDelete =GM.spawnedBonusP2[i];
						GM.spawnedBonusP2.Remove (toDelete);
						Destroy (toDelete.gameObject);
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
			moveX = Input.GetAxis ("Horizontal");
			if (moveX <= 0.1 && moveX >= -0.1)
				moveX = 0;
			else
				transform.position += moveX * Vector3.right * speed * Time.deltaTime;
			//Player 1 moving up or down
			if ((Input.GetAxis ("Enter") > 0) && door && !press) {
				if (outside) {
					outside = false;
					GM.setTownVisibility (playerNumberB, outside);
					moveY = 1;
				} else {
					outside = true;
					GM.setTownVisibility (playerNumberB, outside);
					moveY = 1;
				}
			} else if ((Input.GetAxis ("Get") > 0) && item) {
				if (itemsLeft > 0) {
					if (!outside)
						updateStats (crItem.GetComponent<Items> ());
				}
				moveY = 1;
			} else
				moveY = 0;
			if (Input.GetAxis ("Enter") > 0)
				press = true;
			else
				press = false;
			break;
		case 1:
			// Player 2 moving left or right
			moveX = Input.GetAxis("Horizontal2");
			if (moveX <= 0.1 && moveX >= -0.1)
				moveX = 0;
			else
				transform.position += moveX * Vector3.right * speed * Time.deltaTime;

			//Player 2 moving up or down
			if ((Input.GetAxis ("Enter2") > 0) && door && !press)
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
			else if ((Input.GetAxis ("Get2") > 0) && item)
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
			if (Input.GetAxis ("Enter2") > 0)
				press = true;
			else
				press = false;
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
		if (useRune && hearts > 0) {
			switch (playerNumberB) {
			case 0:
				if (Input.GetKeyDown (KeyCode.W))
					GM.runeBattle.checkRune (playerNumberB, 2, false);
				if (Input.GetKeyDown(KeyCode.A))
					GM.runeBattle.checkRune (playerNumberB, 3, false);
				if (Input.GetKeyDown(KeyCode.S))
					GM.runeBattle.checkRune (playerNumberB, 0, false);
				if (Input.GetKeyDown(KeyCode.D))
					GM.runeBattle.checkRune (playerNumberB, 1, false);
				break;
			case 1:
				if (Input.GetKeyDown (KeyCode.UpArrow))
					GM.runeBattle.checkRune (playerNumberB, 2, false);
				if (Input.GetKeyDown(KeyCode.LeftArrow))
					GM.runeBattle.checkRune (playerNumberB, 3, false);
				if (Input.GetKeyDown(KeyCode.DownArrow))
					GM.runeBattle.checkRune (playerNumberB, 0, false);
				if (Input.GetKeyDown(KeyCode.RightArrow))
					GM.runeBattle.checkRune (playerNumberB, 1, false);
				break;
			}
		}
	}

	public float getReaction() {
		return reactionTime / GM.reactionDifficulty;
	}

	//à revoir

	/* 
	void phase2()
	{
		switch (playerNumberB)
		{
		case 0:
			while (keyLeft != 0)
			{
				if (currentKey == KeyCode.W)
					goodKeyPressed();
				else
					badKeyPressed();
				else if (Input.GetKeyDown(KeyCode.A))
				if (currentKey == KeyCode.A)
					goodKeyPressed();
				else
					badKeyPressed();
				else if (Input.GetKeyDown(KeyCode.S))
				if (currentKey == KeyCode.S)
					goodKeyPressed();
				else
					badKeyPressed();
				else if (Input.GetKeyDown(KeyCode.D))
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
				if (Input.GetKeyDown(KeyCode.UpArrow))
				if (currentKey == KeyCode.UpArrow)
					goodKeyPressed();
				else
					badKeyPressed();
				else if (Input.GetKeyDown(KeyCode.LeftArrow))
				if (currentKey == KeyCode.LeftArrow)
					goodKeyPressed();
				else
					badKeyPressed();
				else if (Input.GetKeyDown(KeyCode.DownArrow))
				if (currentKey == KeyCode.DownArrow)
					goodKeyPressed();
				else
					badKeyPressed();
				else if (Input.GetKeyDown(KeyCode.RightArrow))
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
	} */
}

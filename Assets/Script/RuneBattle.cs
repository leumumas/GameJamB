using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RuneBattle : MonoBehaviour {

	public int runesNumber, runesNumberP1, runesNumberP2;
	public GameObject TimerP1, TimerP2;
	public GameObject RunesP1, RunesP2;
	int P1Type, P2Type;
	Timer TimeP1, TimeP2;
	bool runeP1Ready, runeP2Ready;
	GameManager GM;

	// Use this for initialization
	void Start () {
		GM = GameObject.Find ("ManagerObject").GetComponent<GameManager> ();
		GM.runeBattle = this;
		TimeP1 = TimerP1.GetComponent<Timer> ();
		TimeP2 = TimerP2.GetComponent<Timer> ();
		runesNumberP1 = GM.player [0].difficulty;
		newRunes ();
		for (int j = 0; j < 2; j++) {
			for (int i = 1; i <= GM.player [j].hearts; i++) {
				GameObject.Find ("P" + (j+1) +"Heart" + i).GetComponent<SpriteRenderer> ().color = Color.red;
			}
			if (GM.player [j].shield)
				GameObject.Find ("Shield_P" + (j + 1)).SetActive (true);
			else
				GameObject.Find ("Shield_P" + (j + 1)).SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		/*if (TimeP1.minutes >= GM.player [0].reactionTime && GM.player [0].hearts > 0) {
			runeP1Ready = false;
			GM.player [0].useRune = false;
			TimeP1.StopMultiTimer (0);
			RunesP1.SetActive (false);
			if (runesNumberP1 > 0) {
				P1Type = Random.Range (0, 4);
				setRune (RunesP1, P1Type);
				runesNumberP1--;
				TimeP1.StartMultiTimer (0);
				GM.player [0].useRune = true;
				runeP1Ready = true;
			}
		}
		if (TimeP2.minutes >= GM.player [1].reactionTime && GM.player [1].hearts > 0) {
			runeP2Ready = false;
			GM.player [1].useRune = false;
			TimeP2.StopMultiTimer (1);
			RunesP2.SetActive (false);
			if (runesNumberP2 > 0) {
				P2Type = Random.Range (0, 4);
				setRune (RunesP2, P2Type);
				TimeP2.StartMultiTimer (1);
				GM.player [1].useRune = true;
				runesNumberP2--;
				runeP2Ready = true;
			}
		}*/
		checkVictory ();
		/*if (RunesP1.activeSelf == false && RunesP2.activeSelf == false) {
			if (runesNumber > 0) {
				runesNumber--;
				RunesP1.SetActive (true);
				RunesP2.SetActive (true);
				P1Type = Random.Range (0, 4);
				P2Type = Random.Range (0, 4);
				setRune (RunesP1, P1Type);
				setRune (RunesP2, P2Type);
				GM.player [0].useRune = true;
				GM.player [1].useRune = true;
				TimeP1.StartTimer();
				TimeP2.StartTimer();
				runeP1Ready = true;
				runeP2Ready = true;
			}
		}*/
	}

	public void runeTimeOut(int nbPla) {
		switch (nbPla) {
		case 0: {
			runeP1Ready = false;
			GM.player [0].useRune = false;
			TimeP1.StopMultiTimer (0);
			RunesP1.SetActive (false);
			if (runesNumberP1 > 0) {
				P1Type = Random.Range (0, 4);
				setRune (RunesP1, P1Type);
				runesNumberP1--;
				TimeP1.StartMultiTimer (0);
				GM.player [0].useRune = true;
				runeP1Ready = true;
			}
				break;
		}
		case 1: {
			runeP2Ready = false;
			GM.player [1].useRune = false;
			TimeP2.StopMultiTimer (1);
			RunesP2.SetActive (false);
			if (runesNumberP2 > 0) {
				P2Type = Random.Range (0, 4);
				setRune (RunesP2, P2Type);
				TimeP2.StartMultiTimer (1);
				GM.player [1].useRune = true;
				runesNumberP2--;
				runeP2Ready = true;
				}
				break;
		}
		default: 
			break;
		}
	}

	void checkVictory() {
		if (runesNumberP1 <= 0 && runesNumberP2 <= 0)
			GM.newCycle ();
	}

	void newRunes() {
		RunesP1.SetActive (true);
		RunesP2.SetActive (true);
		P1Type = Random.Range (0, 4);
		P2Type = Random.Range (0, 4);
		setRune (RunesP1, P1Type);
		setRune (RunesP2, P2Type);
		GM.player [0].useRune = true;
		GM.player [1].useRune = true;
		TimeP1.StartMultiTimer(0);
		TimeP2.StartMultiTimer(1);
		runeP1Ready = true;
		runeP2Ready = true;
	}

	void setRune(GameObject rune, int type) {
		rune.SetActive (true);
		switch (type) {
		case 0:
			rune.transform.rotation = Quaternion.Euler (0,0,0);
			Debug.Log ("Rune down");
			break;
		case 1:
			rune.transform.rotation = Quaternion.Euler (0,0,90);
			Debug.Log ("Rune right");
			break;
		case 2:
			rune.transform.rotation = Quaternion.Euler (0,0,180);
			Debug.Log ("Rune Up");
			break;
		case 3: 
			rune.transform.rotation = Quaternion.Euler (0,0,270);
			Debug.Log ("Rune left");
			break;
		default:
			break;
		}
	}

	public void checkRune(int playerNb, int runeType, bool timeOut){
		switch (playerNb) {
		case 0:
			{
				if (runeType == P1Type && !timeOut) {
					//TimeP1.minutes = 10;
					runeTimeOut (playerNb);
					GM.player [playerNb].runeComplete++;
				} else {
					//TimeP1.minutes = 10;
					if (GM.player [playerNb].shield)
						GameObject.Find ("Shield_P" + (playerNb+1)).SetActive (false);
					else {
						GM.player [playerNb].loseLife (1);
						GameObject.Find ("P1Heart" + (GM.player [playerNb].hearts + 1)).GetComponent<SpriteRenderer> ().color = Color.black;
					}
					if (GM.player [playerNb].hearts <= 0) {
						RunesP1.GetComponent<Image> ().color = Color.black;
						runesNumberP1 = 0;
					} else
						runeTimeOut (playerNb);
				}
				break;
			}
		case 1:
			{
				if (runeType == P2Type && !timeOut) {
					//TimeP2.minutes = 10;
					runeTimeOut (playerNb);
					GM.player [playerNb].runeComplete++;
				} else {
					//TimeP2.minutes = 10;
					if (GM.player [playerNb].shield)
						GameObject.Find ("Shield_P" + (playerNb+1)).SetActive (false);
					else {
						GM.player [playerNb].loseLife (1);
						GameObject.Find ("P2Heart" + (GM.player [playerNb].hearts + 1)).GetComponent<SpriteRenderer> ().color = Color.black;
					}
					if (GM.player [playerNb].hearts <= 0) {
						RunesP2.GetComponent<Image> ().color = Color.black;
						runesNumberP2 = 0;
					} else
						runeTimeOut (playerNb);
				}
				break;
			}
		default :
			break;
		}
	}

	void loseRune() {

	}
}

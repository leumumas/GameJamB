using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class END : MonoBehaviour {

    int[] Wins = new int[2];
	public Text Winner;

	void Awake ()
    {
        Destroy(GameObject.Find("Player_0"));
        Destroy(GameObject.Find("Player_1"));
        Wins[0]= GameObject.Find("ManagerObject").GetComponent<GameManager>().player[0].wins;
        Wins[1] =  GameObject.Find("ManagerObject").GetComponent<GameManager>().player[1].wins;
        Destroy(GameObject.Find("ManagerObject"));
    }

	void Start() {
		if (Wins [0] > Wins [1])
			Winner.text = "PLAYER 1 WIN!!";
		else if(Wins[0] < Wins[1])
			Winner.text = "PLAYER 2 WIN!!";
		else
			Winner.text = "DRAW :P";
	}
	

	void Update () {
		
	}

    public void restartGame()
    {
        SceneManager.LoadScene("OpenMenu");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}

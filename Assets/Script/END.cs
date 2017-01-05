using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class END : MonoBehaviour {

    int[] Wins = new int[2];
	// Use this for initialization
	void Awake ()
    {
        Destroy(GameObject.Find("Player_0"));
        Destroy(GameObject.Find("Player_1"));
        Wins[0]=GameManager.instance.player[0].wins;
        Wins[1] = GameManager.instance.player[1].wins;
        Destroy(GameObject.Find("ManagerObject"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void restartGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}

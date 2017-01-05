using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class END : MonoBehaviour {

    int[] Wins = new int[2];

	void Awake ()
    {
        Destroy(GameObject.Find("Player_0"));
        Destroy(GameObject.Find("Player_1"));
        Wins[0]=GameManager.instance.player[0].wins;
        Wins[1] = GameManager.instance.player[1].wins;
        Destroy(GameObject.Find("ManagerObject"));
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

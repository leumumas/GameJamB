using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class END : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        Destroy(GameObject.Find("Player_0"));
        Destroy(GameObject.Find("Player_1"));

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void restartGame()
    {
        Destroy(GameObject.Find("ManagerObject"));
        SceneManager.LoadScene("Menu");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public int nbCycles = 0;
    public GameObject playButton;
    public GameObject menu;
    // Use this for initialization
    void Start () {
        playButton.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (nbCycles != 0 )
            playButton.SetActive(true);
    }

    public void DualCycles(int NbCycle)
    {
        nbCycles = NbCycle;
    }

    public void StartGame()
    {
        enabled = false;
        menu.SetActive(false);

        SceneManager.LoadScene("Samuel");
    }
}

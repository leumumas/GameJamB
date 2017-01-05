using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public int nbCycles = 0;
    public int nbClick = 0;
    public GameObject playButton;
    public GameObject controlPannel;
    public GameObject menu;

    void Start ()
    {
        playButton.SetActive(false);
        DontDestroyOnLoad(gameObject);
        controlPannel.SetActive(false);
    }
	
	void Update ()
    {
        if (nbCycles != 0 )
            playButton.SetActive(true);

        if (nbClick != 0)
            controlPannel.SetActive(true);
    }

    public void DualCycles(int NbCycle)
    {
        nbCycles = NbCycle;
    }
    public void ShowControls(int Nbclicks)
    {
        nbClick = Nbclicks;
    }

    public void StartGame()
    {
        enabled = false;
        menu.SetActive(false);

        SceneManager.LoadScene("Samuel");
    }
}

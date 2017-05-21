using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public int nbCycles = 1;
    public int nbClick = 0;
	public GameObject playButton;
	public GameObject background;
	public GameObject cycle1;
	public GameObject cycle3;
	public GameObject cycle5;
    public GameObject controlPannel;
    public GameObject menu;
	private bool ShowControl = false;

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
		switch (nbCycles)
		{
		case 1:
			background.transform.position = cycle1.transform.position + new Vector3 (20, 15, 0);
			break;
		case 3:	background.transform.position = cycle3.transform.position + new Vector3(20,15,0);
			break;	
		case 5:	background.transform.position = cycle5.transform.position + new Vector3(20,15,0);
			break;
		default: background.transform.position = new Vector3(130,0,0);
			break;
		}
    }

    public void DualCycles(int NbCycle)
    {
        nbCycles = NbCycle;
    }
    public void ShowControls(int Nbclicks)
	{
		if (!ShowControl) {			
			controlPannel.SetActive (true);
			ShowControl = true;
		}
		else {			
			controlPannel.SetActive (false);
			ShowControl = false;
		}
    }

    public void StartGame()
    {
        enabled = false;
        menu.SetActive(false);

        SceneManager.LoadScene("Samuel");
    }
}

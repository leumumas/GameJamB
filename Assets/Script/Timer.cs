using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {


    public float timer = 0;
    public bool timeStarted = false;
    public int minutes;
    public Slider timerSlider;
	public int nbPlayer;
	GameManager GM;

	void Awake()
	{
		timer = 0;
		timeStarted = false;
		GM = GameObject.Find ("ManagerObject").GetComponent<GameManager> ();
	}

    void Start()
    {
		
    }

	public void StartMultiTimer(int nbP) {
		if (nbP == nbPlayer)
			StartTimer ();
	}

	public void StopMultiTimer(int nbP) {
		if (nbP == nbPlayer)
			StopTimer ();
	}

    public void StartTimer()
	{
		enabled = true;
		timer = 0;
		timerSlider.value = 0;
		minutes = 0;
		timeStarted = true;
		Debug.Log ("Restart: " + gameObject.name);
    }

    public void StopTimer()
	{
		Debug.Log ("minutes: " + gameObject.name + " " + minutes);
		timeStarted = false;
		minutes = 0;
        enabled = false;
		timer = 0;
		minutes = 0;
		timerSlider.value = 0;
    }

    void Update()
    {
        if (timeStarted)
        {
			timer = timer + Time.deltaTime;
			minutes = (int)timer;
			switch (GM.CrPhase) {
			case 0:
				timerSlider.value = timer / GM.time;
				break;
			case 1:
				timerSlider.value = timer / GM.player[nbPlayer].getReaction();
				if (timer >= GM.player [nbPlayer].getReaction())
					GM.runeBattle.checkRune (nbPlayer, 0, true);
				break;
			default :
				break;
			}
        }
    }
}

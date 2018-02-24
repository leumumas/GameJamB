using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorTrigger : MonoBehaviour {

	public int nbHouse = 0;
	GameManager GM;
	GameManagerAdventure GMA;
    public GameObject house;

	// Use this for initialization
	void Start () {
		try {
			GM = GameObject.Find ("ManagerObject").GetComponent<GameManager> ();
		}
		catch (NullReferenceException ex)
        {
            Debug.Log("No GameManager");
        }
        try
        {
            GMA = GameObject.Find("ManagerObject").GetComponent<GameManagerAdventure>();
        }
        catch (NullReferenceException ex2)
        {
            Debug.Log("No GameManagerAdventure");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //When in front of door
    void OnTriggerEnter2D(Collider2D collider) {
		collider.gameObject.GetComponent <Player>().door = true;
		int nbPlayer = collider.gameObject.GetComponent<Player>().playerNumberB;
        if (GM != null)
        {
            switch (nbPlayer)
            {
                case 0: GM.nbHouseP1 = nbHouse; break;

                case 1: GM.nbHouseP2 = nbHouse; break;
                default: break;
            }

        }
        else if (GMA != null)
        {
            switch (nbPlayer)
            {
                case 0: GMA.nbHouseP1 = nbHouse; break;

                case 1: GMA.nbHouseP2 = nbHouse; break;
                default: break;
            }
        }
    }
    //When quitting door
    void OnTriggerExit2D(Collider2D collider)
    {
		int nbPlayer = collider.gameObject.GetComponent<Player>().playerNumberB;
		collider.gameObject.GetComponent<Player>().door = false;
		if (collider.gameObject.GetComponent<Player>().outside)
            if (GM != null)
            {
                switch (nbPlayer)
                {
                    case 0: GM.nbHouseP1 = -1; break;

                    case 1: GM.nbHouseP2 = -1; break;
                    default: break;
                }

            }
            else if (GMA != null)
            {
                switch (nbPlayer)
                {
                    case 0: GMA.nbHouseP1 = -1; break;

                    case 1: GMA.nbHouseP2 = -1; break;
                    default: break;
                }
            }
    }

    //When in front of door
    void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.GetComponent<PlayerAdventure>().door = true;
        int nbPlayer = collider.gameObject.GetComponent<PlayerAdventure>().playerNumberB;
        collider.gameObject.GetComponent<PlayerAdventure>().crDoor = this;
        if (GMA != null)
        {
            switch (nbPlayer)
            {
                case 0: GMA.nbHouseP1 = nbHouse; break;

                case 1: GMA.nbHouseP2 = nbHouse; break;
                default: break;
            }
        }
    }
    //When quitting door
    void OnTriggerExit(Collider collider)
    {
        int nbPlayer = collider.gameObject.GetComponent<PlayerAdventure>().playerNumberB;
        collider.gameObject.GetComponent<PlayerAdventure>().door = false;
        collider.gameObject.GetComponent<PlayerAdventure>().crDoor = null;
        if (collider.gameObject.GetComponent<PlayerAdventure>().outside)
            if (GMA != null)
            {
                switch (nbPlayer)
                {
                    case 0: GMA.nbHouseP1 = -1; break;

                    case 1: GMA.nbHouseP2 = -1; break;
                    default: break;
                }
                
            }
    }

    public void houseVisibility (bool view)
    {
        house.SetActive(!view);
    }
}

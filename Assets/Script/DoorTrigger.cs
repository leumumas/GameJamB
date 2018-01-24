using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

    public int nbHouse = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //When in front of door
    void OnTriggerEnter2D(Collider2D collider) {
		collider.gameObject.GetComponent <Player>().door = true;
		int nbPlayer = collider.gameObject.GetComponent<Player>().playerNumberB;
        switch(nbPlayer)
        {
            case 0: GameObject.Find("ManagerObject").GetComponent<GameManager>().nbHouseP1 = nbHouse; break;
                
            case 1: GameObject.Find("ManagerObject").GetComponent<GameManager>().nbHouseP2 = nbHouse; break;
            default: break;
        }
    }
    //When quitting door
    void OnTriggerExit2D(Collider2D collider)
    {
		int nbPlayer = collider.gameObject.GetComponent<Player>().playerNumberB;
		collider.gameObject.GetComponent<Player>().door = false;
		if (collider.gameObject.GetComponent<Player>().outside) 
	        switch (nbPlayer)
	        {
	            case 0: GameObject.Find("ManagerObject").GetComponent<GameManager>().nbHouseP1 = -1; break;

	            case 1: GameObject.Find("ManagerObject").GetComponent<GameManager>().nbHouseP2 = -1; break;
	            default: break;
	        }
    }
}

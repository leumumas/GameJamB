using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	public int playerSide;
	public int nbHouse;

    // Use this for initialization
    void Start() {
        /*switch (playerSide)
        {
            case 0:  GameObject.Find("ManagerObject").GetComponent<GameManager>().SpawnPointP1.Add(this.transform.gameObject); break;
            case 1:  GameObject.Find("ManagerObject").GetComponent<GameManager>().SpawnPointP2.Add(this.transform.gameObject); break;
            default: break;
        }*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

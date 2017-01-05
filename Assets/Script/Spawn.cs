using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public int playerSide;

    // Use this for initialization
    void Start() {
        switch (playerSide)
        {
            case 0: GameManager.instance.SpawnPointP1.Add(this.transform.gameObject); break;
            case 1: GameManager.instance.SpawnPointP2.Add(this.transform.gameObject); break;
            default: break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {
    //is the item a bonus
    private bool isBonus;
    //what type of item
    private int type;

    private void Awake()
    {

    }

    void Start () {
        switch (type) {
            case 0: SetupItems("Instant", true, 0, -1); break;
            case 1: SetupItems("Time", false, 0, 1); break;
            case 2: SetupItems("Instant", true, 0, 1); break;
            case 3: SetupItems("Instant", true, 0, 1); break;
            case 4: SetupItems("shield", true, 0, 0); break;
            default: break;

        }
		
	}

	void Update () {
		
	}

    void SetupItems(string name, bool bonus, int prompt, int reaction)
    {

    }
}

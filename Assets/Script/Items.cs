using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {
    //is the item a bonus
    private bool isBonus;
    //what type of item
    private int type;
    private string name;

    private void Awake()
    {

    }

    void Start () {
        switch (type) {
            case 0: SetupItems("Instant Orb", true, 0, -1); break;
            case 1: SetupItems("Lag Orb", false, 0, 1); break;
            case 2: SetupItems("Cover", true, -1, 1); break;
            case 3: SetupItems("Rain", false, 1, 1); break;
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

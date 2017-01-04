using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
    public GameObject[] image;
    private int noPlayer;
    public int key;

    public Button (int player)
    {
        noPlayer = player;
        key = Random.Range(0, 4);
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

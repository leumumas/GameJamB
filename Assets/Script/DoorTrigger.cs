using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //When in front of door
    void OnTriggerEnter2D(Collider2D collider) {
        collider.gameObject.GetComponent<Character>().door = true;
    }
    //When quitting door
    void OnTriggerExit2D(Collider2D collider)
    {
        collider.gameObject.GetComponent<Character>().door = false;
    }
}

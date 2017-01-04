using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }
    //When in front of item
    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.gameObject.GetComponent<Character>().item = true;
        collider.gameObject.GetComponent<Character>().crItem = transform.root.gameObject;
    }
    //When far of the item
    void OnTriggerExit2D(Collider2D collider)
    {
        collider.gameObject.GetComponent<Character>().item = false;
        collider.gameObject.GetComponent<Character>().crItem = null;
    }
}

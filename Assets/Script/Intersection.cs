using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : MonoBehaviour {
    
    public GameObject visualStreet1, visualStreet2;
    public float angle1, angle2;
    public int number1, number2;
    PlayerAdventure crPlayer;
    public Vector3 position1, position2;
    GameManagerAdventure GMA;

    // Use this for initialization
    void Start ()
    {
        GMA = GameObject.Find("ManagerObject").GetComponent<GameManagerAdventure>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //When in intersection
    void OnTriggerEnter(Collider collider)
    {
        crPlayer = collider.gameObject.GetComponent<PlayerAdventure>();
        crPlayer.intersection = true;
        int nbPlayer = crPlayer.playerNumberB;
        crPlayer.crIntersection = this;
    }
    //When quitting intersection
    void OnTriggerExit(Collider collider)
    {
        int nbPlayer = crPlayer.playerNumberB;
        crPlayer.intersection = false;
        crPlayer.crIntersection = null;
        crPlayer = null;
    }

    public void switchStreet(int crStreet)
    {
        if(crStreet == number1)
        {
            crPlayer.angleMove = angle2;
            crPlayer.setPositionMove(position2);
            crPlayer.startRotation();
            visualStreet2.GetComponent<Street>().changeOpacity(1);
            visualStreet1.GetComponent<Street>().changeOpacity(GMA.opacity);
            visualStreet2.SetActive(true);
            visualStreet1.SetActive(false);
            //crPlayer.gameObject.transform.position = position2;
            //crPlayer.gameObject.transform.eulerAngles = new Vector3(0, angle2, 0);
            crPlayer.crStreet = number2;
        }
        else
        {
            crPlayer.angleMove = angle1;
            crPlayer.setPositionMove(position1);
            crPlayer.startRotation();
            visualStreet2.GetComponent<Street>().changeOpacity(GMA.opacity);
            visualStreet1.GetComponent<Street>().changeOpacity(1);
            visualStreet1.SetActive(true);
            visualStreet2.SetActive(false);
            //crPlayer.gameObject.transform.position = position1;
            //crPlayer.gameObject.transform.eulerAngles = new Vector3(0, angle1, 0);
            crPlayer.crStreet = number1;
        }
    }
}

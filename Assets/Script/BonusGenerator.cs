using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGenerator : MonoBehaviour {

    public GameObject bonusItem;
    private bool begin = true;
	GameObject InstObject;
	Items[] Bonus = new Items[13];
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (begin)
        {
            spawnItems(0, 0, 0);
            spawnItems(0, 0, 1);
            spawnItems(0, 0, 2);
            spawnItems(1, 0, 3);
            spawnItems(1, 0, 4);
            spawnItems(1, 0, 5);
            spawnItems(2, 0, 6);
            spawnItems(2, 0, 7);
            spawnItems(2, 0, 8);
            spawnItems(3, 0, 9);
            spawnItems(3, 0, 10);
            spawnItems(3, 0, 11);
            spawnItems(4, 0, 12);
            begin = false;
        }
		
	}

	void spawnItems (int ty, int pl, int nb)
    {
        Transform spawnP;
        int rnd;
        /*switch (pl)
        {
            case 0: rnd = Random.Range(0,  GameObject.Find("ManagerObject").GetComponent<GameManager>().SpawnPointP1.Count);
                    spawnP = GameObject.Find("ManagerObject").GetComponent<GameManager>().SpawnPointP1[rnd].transform;
                    GameObject.Find("ManagerObject").GetComponent<GameManager>().SpawnPointP1.RemoveAt(rnd);
                    InstObject = (GameObject)Instantiate(bonusItem, spawnP.position, Quaternion.identity);
					InstObject.GetComponent<Items>().typeSetup(ty);
					Bonus[0] = InstObject.GetComponent<Items>();
                    break;
            case 1: rnd = Random.Range(0,  GameObject.Find("ManagerObject").GetComponent<GameManager>().SpawnPointP1.Count);
                    spawnP = GameObject.Find("ManagerObject").GetComponent<GameManager>().SpawnPointP2[rnd].transform;
                    GameObject.Find("ManagerObject").GetComponent<GameManager>().SpawnPointP2.RemoveAt(rnd);
                    InstObject = (GameObject)Instantiate(bonusItem, spawnP.position, Quaternion.identity);
                    InstObject.GetComponent<Items>().typeSetup(ty);
                    break;
            default: break;
		}*/
		rnd = Random.Range(0,  GameObject.Find("ManagerObject").GetComponent<GameManager>().SpawnPointP1.Count-1);
		spawnP = GameObject.Find("ManagerObject").GetComponent<GameManager>().SpawnPointP1[rnd].transform;
		GameObject.Find("ManagerObject").GetComponent<GameManager>().SpawnPointP1.RemoveAt(rnd);
		//player 1;
		InstObject = (GameObject)Instantiate(bonusItem, spawnP.position, Quaternion.identity);
		Items crItem = InstObject.GetComponent<Items> ();
		crItem.nbHouse = spawnP.GetComponent<Spawn> ().nbHouse;
		InstObject.GetComponent<Items>().typeSetup(ty);
		InstObject.GetComponent<Items> ().itemNb = nb;
		InstObject.layer = 8;
		GameObject.Find ("ManagerObject").GetComponent<GameManager> ().spawnedBonusP1[nb] = crItem;

		//player 2
		InstObject = (GameObject)Instantiate(bonusItem, spawnP.position, Quaternion.identity);
		crItem = InstObject.GetComponent<Items> ();
		crItem.nbHouse = spawnP.GetComponent<Spawn> ().nbHouse;
		InstObject.GetComponent<Items>().typeSetup(ty);
		InstObject.GetComponent<Items> ().itemNb = nb;
		InstObject.layer = 9;
		GameObject.Find ("ManagerObject").GetComponent<GameManager> ().spawnedBonusP2[nb] = crItem;
    }

    public void beginning()
    {
        begin = true;
    }
}

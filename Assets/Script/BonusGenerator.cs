using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGenerator : MonoBehaviour {

    public GameObject bonusItem;
    private bool begin = true;
    GameObject InstObject;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (begin)
        {
            spawnItems(0, 0);
            spawnItems(0, 0);
            spawnItems(0, 0);
            spawnItems(1, 0);
            spawnItems(1, 0);
            spawnItems(1, 0);
            spawnItems(2, 0);
            spawnItems(2, 0);
            spawnItems(2, 0);
            spawnItems(3, 0);
            spawnItems(3, 0);
            spawnItems(3, 0);
            spawnItems(4, 0);
            //player 2 items
            spawnItems(0, 1);
            spawnItems(0, 1);
            spawnItems(0, 1);
            spawnItems(1, 1);
            spawnItems(1, 1);
            spawnItems(1, 1);
            spawnItems(2, 1);
            spawnItems(2, 1);
            spawnItems(2, 1);
            spawnItems(3, 1);
            spawnItems(3, 1);
            spawnItems(3, 1);
            spawnItems(4, 1);
            begin = false;
        }
		
	}

    void spawnItems (int ty, int pl)
    {
        Transform spawnP;
        int rnd;
        switch (pl)
        {
            case 0: rnd = Random.Range(0, GameManager.instance.SpawnPointP1.Count);
                    spawnP = GameManager.instance.SpawnPointP1[rnd].transform;
                    GameManager.instance.SpawnPointP1.RemoveAt(rnd);
                    InstObject = (GameObject)Instantiate(bonusItem, spawnP.position, Quaternion.identity);
                    InstObject.GetComponent<Items>().typeSetup(ty);
                    break;
            case 1: rnd = Random.Range(0, GameManager.instance.SpawnPointP1.Count);
                    spawnP = GameManager.instance.SpawnPointP2[rnd].transform;
                    GameManager.instance.SpawnPointP2.RemoveAt(rnd);
                    InstObject = (GameObject)Instantiate(bonusItem, spawnP.position, Quaternion.identity);
                    InstObject.GetComponent<Items>().typeSetup(ty);
                    break;
            default: break;
        }
    }
}

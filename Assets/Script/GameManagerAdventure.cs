using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManagerAdventure : MonoBehaviour {
	public static GameManagerAdventure instance = null;
	int cycles,
	playerNumber = 2;
	bool firstRound = true, start = true;
	private GameObject instanciatedObject;
	public List<PlayerAdventure> player = new List<PlayerAdventure>();
	private GameObject menuScript;
	public GameObject playerPrefab;
	GameObject[] townSprite;
	public int nbHouseP1;
	public int nbHouseP2;
	GameObject[] HousesP1;
	List<GameObject> HousesP2 = new List<GameObject>();
	public List<GameObject> SpawnPointP1;
	public List<GameObject> SpawnPointP2;
	public float time = 360;
	Slider itemSlidP1, itemSlidP2;
	public GameObject timerObject;
	public List<Items> spawnedBonusP1;
	public List<Items> spawnedBonusP2;
	float CharaY = 0;
	public int CrPhase, beginningStreet;
	public float reactionDifficulty = 5, opacity;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		menuScript = GameObject.Find("MenuObject");
		CrPhase = 0;
	}


	// Use this for initialization
	void Start()
	{
			/*if (firstRound) {
				for (int i = 0; i < playerNumber; i++) { //instancie les personnages dynamiquement
					instanciatedObject = (GameObject)Instantiate (playerPrefab, new Vector3 (i * -80 + 40, i * CharaY, 0), Quaternion.identity);
					instanciatedObject.name = "Player_" + i;
					player.Add (instanciatedObject.GetComponent<Player> ());
					player [i].GetComponentInChildren<Camera> ().Render ();
					player [i].GetComponentInChildren<Camera> ().rect = new Rect (0, i * -0.5f + 0.5f, 1, 0.5f);
					player [i].GetComponent<Player> ().playerNumberB = i;
					player [i].GetComponentInChildren<Camera> ().transform.parent = player [i].transform;
				}
			}*/
		foreach(GameObject pla in GameObject.FindGameObjectsWithTag ("Player")) {
			player.Add(pla.GetComponent <PlayerAdventure>());
        }
        nbHouseP1 = -1;
		nbHouseP2 = -1;
		player [0].GetComponentInChildren<AudioListener> ().enabled = true;
		townSprite = GameObject.FindGameObjectsWithTag ("Background");
		setDoors ();
        CrPhase = 0;
        player[0].crStreet = beginningStreet;
        setDoors();
        foreach (GameObject hou in HousesP1)
        {
            hou.SetActive(false);
        }
        start = false;
        townSprite = GameObject.FindGameObjectsWithTag("Background");
        player[0].GetComponent<BoxCollider>().enabled = true;
        foreach (GameObject street in GameObject.FindGameObjectsWithTag("street"))
        {
            if (player[0].crStreet != street.GetComponent<Street>().nbStreet)
            {
                street.GetComponent<Street>().changeOpacity(opacity);
                street.SetActive(false);
            }
            else
            {
                street.GetComponent<Street>().changeOpacity(255);
                street.SetActive(true);
            }
        }
    }

	// Update is called once per frame
	void Update()
	{
		if (start) {
		} else {
			if ((nbHouseP1 == nbHouseP2 && (!player [0].outside)) || (player [0].outside)) {
				player [0].setCullingMask ("110111111111");
			} else {
				player [0].setCullingMask ("010111111111");
			}
		}
	}

	public void malusUpdate(Items it, int nbPlayer) //met à jour les informations du joueur adverse
	{
		switch (nbPlayer)
		{
		case 0: player[1].difficulty += it.promptItem;
			player[1].reactionTime += it.reactionItem; break;
		case 1: player[0].difficulty += it.promptItem;
			player[0].reactionTime += it.reactionItem; break;
		default: break;
		}
	}

	public void setTownVisibility(int nbPlayer, bool view)
	{
		switch (nbPlayer)
		{
		case 0: HousesP1[nbHouseP1].SetActive(!view); 
			break;

		case 1: HousesP2[nbHouseP2].SetActive(!view); 
			break;
		default: break;
		}
	}

	public void setDoors()
    {
        int doors = GameObject.FindGameObjectsWithTag("House").Length;
        HousesP1 = new GameObject[doors];
        foreach (GameObject door in GameObject.FindGameObjectsWithTag("House"))
        {
            HousesP1[door.GetComponent<House>().nbHouse] = door;
        }
        /*for (int i = 0; i < playerNumber; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				string house = "Square " + (j + 1) + " " + (i + 1);
				switch (i)
				{
				case 0:
                    HousesP1.Add(GameObject.Find(house));
					break;
				case 1:
					HousesP2.Add(GameObject.Find(house));
					break;
				default: break;
				}
			}
		}*/
    }

    void Characters()
	{
		for (int i = 0; i < playerNumber; i++) //instancie les personnages dynamiquement es assigne les anciennes valeurs, plusieurs cycles
		{
			/*instanciatedObject = (GameObject)Instantiate(playerPrefab, new Vector3(i * -80 + 40, i * CharaY, 0), Quaternion.identity);
            instanciatedObject.name = "Player_" + i;
			instanciatedObject.GetComponent<Player>().wins = player[i].wins;
			instanciatedObject.GetComponent<Player>().playerNumberB = i;
			instanciatedObject.GetComponent<Player>().characterUpdate();
            player[i] = instanciatedObject.GetComponent<Player>();*/
			player[i].GetComponentInChildren<Camera>().Render();
			player[i].GetComponentInChildren<Camera>().rect = new Rect(0, i * -0.5f + 0.5f, 1, 0.5f);
			player[i].GetComponent<Player>().playerNumberB = i;
			player[i].GetComponentInChildren<Camera>().transform.parent = player[i].transform;
		}
	}

	public void newCycle() {
		if (cycles > 0) {
			SceneManager.LoadScene ("VersusSameMap");
			CrPhase = 0;
			time = 5;
			nbHouseP1 = -1;
			nbHouseP2 = -1;
			player [0].GetComponentInChildren<AudioListener> ().enabled = true;
			townSprite = GameObject.FindGameObjectsWithTag ("Background");
			setDoors ();
			start = true;
		}
		else
			SceneManager.LoadScene ("End");
	}
}

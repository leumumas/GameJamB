using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    int cycles,
        playerNumber = 2;
    Timer timer;
    bool firstRound = true, start = true;
    private GameObject instanciatedObject;
	public List<Player> player = new List<Player>();
    private GameObject menuScript;
    public GameObject playerPrefab;
	GameObject[] townSprite;
    public int nbHouseP1;
    public int nbHouseP2;
	List<GameObject> HousesP1 = new List<GameObject>();
	List<GameObject> HousesP2 = new List<GameObject>();
    public List<GameObject> SpawnPointP1;
    public List<GameObject> SpawnPointP2;
    public float time = 360;
    Slider itemSlidP1, itemSlidP2;
	public GameObject timerObject;
	public List<Items> spawnedBonusP1;
	public List<Items> spawnedBonusP2;
	float CharaY = 0;
	public int CrPhase;
	public RuneBattle runeBattle;
	public float reactionDifficulty = 5;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        menuScript = GameObject.Find("GameObject");
        setStartLevel();
		CrPhase = 0;
    }


    // Use this for initialization
    void Start()
    {
		if (SceneManager.GetActiveScene().name == "VersusSameMap") {
			time = 5;
			spawnedBonusP1 = new List<Items> ();
			spawnedBonusP2 = new List<Items> ();
			timer = GameObject.Find ("Timer").GetComponent<Timer> ();
			timer.StartTimer ();
			itemSlidP1 = GameObject.Find ("ItemsSlidP1").GetComponent<Slider> ();
			itemSlidP2 = GameObject.Find ("ItemsSlidP2").GetComponent<Slider> ();
			if (firstRound) {
				for (int i = 0; i < playerNumber; i++) { //instancie les personnages dynamiquement
					instanciatedObject = (GameObject)Instantiate (playerPrefab, new Vector3 (i * -80 + 40, i * CharaY, 0), Quaternion.identity);
					instanciatedObject.name = "Player_" + i;
					player.Add (instanciatedObject.GetComponent<Player> ());
					player [i].GetComponentInChildren<Camera> ().Render ();
					player [i].GetComponentInChildren<Camera> ().rect = new Rect (0, i * -0.5f + 0.5f, 1, 0.5f);
					player [i].GetComponent<Player> ().playerNumberB = i;
					player [i].GetComponentInChildren<Camera> ().transform.parent = player [i].transform;
				}
			} else {

			}
			nbHouseP1 = -1;
			nbHouseP2 = -1;
			player [0].GetComponentInChildren<AudioListener> ().enabled = true;
			townSprite = GameObject.FindGameObjectsWithTag ("Background");
			setDoors ();
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (SceneManager.GetActiveScene().name == "VersusSameMap") {
			if (start) {
				CrPhase = 0;
				timer = GameObject.Find ("Timer").GetComponent<Timer> ();
				timer.StartTimer ();
				itemSlidP1 = GameObject.Find ("ItemsSlidP1").GetComponent<Slider> ();
				itemSlidP2 = GameObject.Find ("ItemsSlidP2").GetComponent<Slider> ();
				setDoors ();
				foreach (GameObject hou in HousesP1) {
					hou.SetActive (false);
				}
				foreach (GameObject hou in HousesP2) {
					hou.SetActive (false);
				}
				start = false;
				townSprite = GameObject.FindGameObjectsWithTag ("Background");
				SpawnPointP1 = new List<GameObject> (GameObject.FindGameObjectsWithTag ("InsideP1"));
				SpawnPointP2 = new List<GameObject> (GameObject.FindGameObjectsWithTag ("insideP2"));
				GetComponentInParent<BonusGenerator> ().beginning ();
				if (!firstRound)
					Characters ();
				firstRound = false;
			} else {
				if ((nbHouseP1 == nbHouseP2 && (!player [0].outside && !player [1].outside)) || (player [0].outside && player [1].outside)) {
					player [0].setCullingMask ("110111111111");
					player [1].setCullingMask ("111011111111");
				} else {
					player [0].setCullingMask ("010111111111");
					player [1].setCullingMask ("101011111111");
				}
			}

			if (timer.minutes >= (int)time) {
				foreach (GameObject hou in HousesP1) {
					hou.SetActive (true);
				}
				foreach (GameObject hou in HousesP2) {
					hou.SetActive (true);
				}
				cycles -= 1;
				timer.StopTimer ();
				timer.minutes = 0;
				if (cycles >= 0) {
					CrPhase = 1;
					SpawnPointP1.Clear ();
					SpawnPointP2.Clear ();
					SceneManager.LoadScene ("Battle");
				}
			}
			itemSlidP1.value = (float)player [0].itemsLeft / 6f;
			itemSlidP2.value = player [1].itemsLeft / 6f;
		}
    }

    void setStartLevel() //récupère le nombre de cycles choisi par les joueurs
    {
        cycles = menuScript.GetComponent<Menu>().nbCycles;
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
			if (view)
			{
				for (int i = 0; i < spawnedBonusP1.Count; i++)
					if (spawnedBonusP1[i].nbHouse == nbHouseP1)
						spawnedBonusP1[i].GetComponent<SpriteRenderer>().sortingOrder = 4;
			} else {
				for (int i = 0; i < spawnedBonusP1.Count; i++)
					if (spawnedBonusP1[i].nbHouse == nbHouseP1)
						spawnedBonusP1[i].GetComponent<SpriteRenderer>().sortingOrder = 8;
			}
			break;

		case 1: HousesP2[nbHouseP2].SetActive(!view); 
			if (view)
			{
				for (int i = 0; i < spawnedBonusP2.Count; i++)
					if (spawnedBonusP2[i].nbHouse == nbHouseP2)
						spawnedBonusP2[i].GetComponent<SpriteRenderer>().sortingOrder = 4;
			} else {
				for (int i = 0; i < spawnedBonusP2.Count; i++)
					if (spawnedBonusP2[i].nbHouse == nbHouseP2)
						spawnedBonusP2[i].GetComponent<SpriteRenderer>().sortingOrder = 8;
			}
			break;
        default: break;
        }
    }

    public void setDoors()
    {
        for (int i = 0; i < playerNumber; i++)
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
        }
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
			spawnedBonusP1 = new List<Items> ();
			spawnedBonusP2 = new List<Items> ();
			timer = GameObject.Find ("Timer").GetComponent<Timer> ();
			timer.StartTimer ();
			itemSlidP1 = GameObject.Find ("ItemsSlidP1").GetComponent<Slider> ();
			itemSlidP2 = GameObject.Find ("ItemsSlidP2").GetComponent<Slider> ();
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
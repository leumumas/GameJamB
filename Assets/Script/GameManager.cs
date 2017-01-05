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
    public List<Character> player = new List<Character>();
    private GameObject menuScript;
    public GameObject playerPrefab;
    GameObject[] townSprite;
    public int nbHouseP1;
    public int nbHouseP2;
    GameObject[] HousesP1 = new GameObject[10];
    GameObject[] HousesP2 = new GameObject[10];
    public List<GameObject> SpawnPointP1;
    public List<GameObject> SpawnPointP2;
    public float time = 60;
    Slider itemSlidP1, itemSlidP2;
    public GameObject timerObject;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        menuScript = GameObject.Find("GameObject");
        setStartLevel();
    }


    // Use this for initialization
    void Start()
    {
        time = 30;
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        timer.StartTimer();
        itemSlidP1 = GameObject.Find("ItemsSlidP1").GetComponent<Slider>();
        itemSlidP2 = GameObject.Find("ItemsSlidP2").GetComponent<Slider>();
        if (firstRound)
        {
            for (int i = 0; i < playerNumber; i++) //instancie les personnages dynamiquement
            {
                instanciatedObject = (GameObject)Instantiate(playerPrefab, new Vector3(i * -80 + 40, i * -20, 0), Quaternion.identity);
                instanciatedObject.name = "Player_" + i;
                player.Add(instanciatedObject.GetComponent<Character>());
                player[i].GetComponentInChildren<Camera>().Render();
                player[i].GetComponentInChildren<Camera>().rect = new Rect(0, i * -0.5f + 0.5f, 1, 0.5f);
                player[i].GetComponent<Character>().playerNumberB = i;
                player[i].GetComponentInChildren<Camera>().transform.parent = player[i].transform;
            }
        }
        else
        {

        }
        player[0].GetComponentInChildren<AudioListener>().enabled = true;
        townSprite = GameObject.FindGameObjectsWithTag("Background");
        setDoors();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            timer = GameObject.Find("Timer").GetComponent<Timer>();
            timer.StartTimer();
            setDoors();
            foreach (GameObject hou in HousesP1)
            {
                Debug.Log(hou.name);
                hou.SetActive(false);
            }
            foreach (GameObject hou in HousesP2)
            {
                hou.SetActive(false);
            }
            start = false;
            townSprite = GameObject.FindGameObjectsWithTag("Background");
            SpawnPointP1 = new List<GameObject>(GameObject.FindGameObjectsWithTag("InsideP1"));
            SpawnPointP2 = new List<GameObject>(GameObject.FindGameObjectsWithTag("insideP2"));
            GetComponentInParent<BonusGenerator>().beginning();
            if (!firstRound)
                Characters();
            firstRound = false;
        }

        if (timer.minutes >= (int)time)
        {
            foreach (GameObject hou in HousesP1)
            {
                Debug.Log(hou.name);
                hou.SetActive(true);
            }
            foreach (GameObject hou in HousesP2)
            {
                hou.SetActive(true);
            }
            cycles -= 1;
            timer.StopTimer();
            timer.minutes = 0;
            if (cycles > 0)
            {
                SpawnPointP1.Clear();
                SpawnPointP2.Clear();
                start = true;
                SceneManager.LoadScene("Samuel");
            }
            else
            {
                Destroy(menuScript);
                SceneManager.LoadScene("End");
            }
        }
        itemSlidP1.value = (float)player[0].itemsLeft / 6f;
        Debug.Log(itemSlidP1.value);
        itemSlidP2.value = player[1].itemsLeft / 6f;
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
        townSprite[nbPlayer].SetActive(view);
        switch (nbPlayer)
        {
            case 0: HousesP1[nbHouseP1].SetActive(!view); break;

            case 1: HousesP2[nbHouseP2].SetActive(!view); break;
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
                        HousesP1[j] = GameObject.Find(house);
                        break;
                    case 1:
                        HousesP2[j] = GameObject.Find(house);
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
            instanciatedObject = (GameObject)Instantiate(playerPrefab, new Vector3(i * -80 + 40, i * -20, 0), Quaternion.identity);
            instanciatedObject.name = "Player_" + i;
            instanciatedObject.GetComponent<Character>().wins = player[i].wins;
            instanciatedObject.GetComponent<Character>().playerNumberB = i;
            instanciatedObject.GetComponent<Character>().characterUpdate();
            player[i] = instanciatedObject.GetComponent<Character>();
        }
    }
}
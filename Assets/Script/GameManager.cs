using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    int cycles,
        playerNumber = 2;
    Timer timer;
    private GameObject instanciatedObject;
    private List<Character> player = new List<Character>();
    private GameObject menuCamera;
    public GameObject playerPrefab;
    public GameObject[] townSprite = new GameObject[2];
    public int nbHouseP1;
    public int nbHouseP2;
    public GameObject[] HousesP1 = new GameObject[10];
    public GameObject[] HousesP2 = new GameObject[10];

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        /*menuCamera = GameObject.Find("Menu Camera");
        setStartLevel();*/
    }


    // Use this for initialization
    void Start()
    {
        timer = GetComponent<Timer>();
        timer.StartTimer();
        for (int i = 0; i < playerNumber; i++) //instancie les personnages dynamiquement
        {
            instanciatedObject = (GameObject)Instantiate(playerPrefab,new Vector3(i*-80+40, i*-20, 0), Quaternion.identity);
            instanciatedObject.name = "Player_" + i;
            player.Add(instanciatedObject.GetComponent<Character>());
            player[i].GetComponentInChildren<Camera>().Render();
            player[i].GetComponentInChildren<Camera>().rect=new Rect(0,i*-0.5f+0.5f,1,0.5f);
            player[i].GetComponent<Character>().playerNumberB = i;
            player[i].GetComponentInChildren<Camera>().transform.parent = player[i].transform;
        }
        Destroy(menuCamera);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setStartLevel() //récupère le nombre de cycles choisi par les joueurs
    {
        cycles = menuCamera.GetComponent<Menu>().nbCycles;
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
            case 0:  HousesP1[nbHouseP1].SetActive(!view); break;
                
            case 1: HousesP2[nbHouseP2].SetActive(!view); break;
            default: break;
        }
    }
}
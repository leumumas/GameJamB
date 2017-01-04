using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int cycles,
        playerNumber = 2;
    Timer timer;
    private GameObject instanciatedObject;
    private List<Character> player = new List<Character>();
    private GameObject menuCamera;
    public GameObject playerPrefab;

    private void Awake()
    {
        menuCamera = GameObject.Find("Menu Camera");
        setStartLevel();
    }


    // Use this for initialization
    void Start()
    {
        timer = GetComponent<Timer>();
        timer.StartTimer();
        for (int i = 0; i < playerNumber; i++)
        {
            instanciatedObject = (GameObject)Instantiate(playerPrefab,new Vector3(i*-80+40, i*-20, 0), Quaternion.identity);
            instanciatedObject.name = "Player_" + i;
            player.Add(instanciatedObject.GetComponent<Character>());
            player[i].GetComponentInChildren<Camera>().Render();
            player[i].GetComponentInChildren<Camera>().rect=new Rect(0,i*0.5f,1,0.5f);
            player[i].GetComponent<Character>().playerNumberB = i;
            player[i].GetComponentInChildren<Camera>().transform.parent = player[i].transform;
        }
        Destroy(menuCamera);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setStartLevel()
    {
        cycles = menuCamera.GetComponent<Menu>().nbCycles;
    }
}
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
    private Transform cameraPos;

    private void Awake()
    {
        menuCamera = GameObject.Find("Menu Camera");
        setStartLevel();
        DestroyImmediate(GameObject.Find("Menu Camera"));
    }


    // Use this for initialization
    void Start()
    {
        cycles = 2;
        timer = GetComponent<Timer>();
        timer.StartTimer();
        cameraPos = Camera.main.transform;
        for (int i = 0; i < playerNumber; i++)
        {
            instanciatedObject = (GameObject)Instantiate(playerPrefab, new Vector3(cameraPos.position.x, cameraPos.position.y, cameraPos.position.z+2), Quaternion.identity);
            //instanciatedObject.transform.parent.position = new Vector3(0,0,0);
            instanciatedObject.name = "Player_" + i;
            player.Add(instanciatedObject.GetComponent<Character>());
            player[i].GetComponent<Character>().playerNumberB = i;
        }
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
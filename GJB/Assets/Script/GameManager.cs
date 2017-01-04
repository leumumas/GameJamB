using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int cycles,
        playerNumber;
    Timer timer;
    private GameObject instanciatedObject;
    private List<Character> player = new List<Character>();
    private GameObject menuCamera;

    private void Awake()
    {
        menuCamera = GameObject.Find("Menu Camera");
        setStartLevel();
    }


    // Use this for initialization
    void Start()
    {
        cycles = 2;
        timer = GetComponent<Timer>();
        timer.StartTimer();
        for (int i = 0; i < playerNumber; i++)
        {
            //player.Add(instanciatedObject.GetComponent<Character>());
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
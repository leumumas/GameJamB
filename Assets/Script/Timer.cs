﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {


    public static float timer = 0;
    public bool timeStarted = false;
    public int minutes;
    public Slider timerSlider;
    

    void Start()
    {

    }

    public void StartTimer()
    {
        timeStarted = true;
    }

    public void StopTimer()
    {
        timeStarted = false;
        enabled = false;
        timer = 0;
    }

    void Update()
    {
        if (timeStarted)
        {
            timer = timer + Time.deltaTime * 2;
            timerSlider.value = timer /  GameObject.Find("ManagerObject").GetComponent<GameManager>().time;
            minutes = (int)timer;
        }
    }
}

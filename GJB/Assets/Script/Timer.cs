using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {


    public static float timer = 480;
    public bool timeStarted = false;
    int hours, minutes;

    public int Minutes
    {
        get
        {
            return minutes;
        }
    }

    public int Hours
    {
        get
        {
            return hours;
        }
    }

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
    }

    void Update()
    {
        if (timeStarted)
        {
            Debug.Log("salut");
            timer = timer + Time.deltaTime * 2;
            hours = (int)timer / 60;
            minutes = (int)timer % 60;
            // Debug.Log(hours + ":" + minutes);
        }
    }
}

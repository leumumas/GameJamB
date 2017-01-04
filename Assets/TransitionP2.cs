using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionP2 : MonoBehaviour
{
    private float time,
                  speed;
    // Use this for initialization
    void Start()
    {
        time = 0f;
        speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * vitesseDefilement() * Time.deltaTime;
    }

    float vitesseDefilement()
    {
        return -1.5f * (Time.time - 5f) + -0.5f;
    }
}

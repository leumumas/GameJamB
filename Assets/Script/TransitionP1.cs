using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionP1 : MonoBehaviour {
    private float time,
                  speed;

    private bool reachMiddle;
    // Use this for initialization
    void Start() {
        time = 0f;
        speed = 1f;
        reachMiddle = false;
    }

    // Update is called once per frame
    void Update() {

        if (speed <= 0.2f)
            reachMiddle = true;

        if(!reachMiddle)
        {
            vitesseDefilement();
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.up * 8 * Time.deltaTime;
        }
    }

    void vitesseDefilement()
    {
        speed = System.Math.Abs(1f * (Time.time - 5f));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Street : MonoBehaviour {

    public GameObject streetRendu;
    public int nbStreet;
    int speedRotation = 2;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeOpacity(float view)
    {
        streetRendu.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f, view);
    }

    IEnumerator opacity(float view)
    {
        float startTime = Time.time;
        Color crColor = streetRendu.GetComponent<SpriteRenderer>().color;
        Color nxtColor = crColor;
        nxtColor.a = view;
        while (Time.time < startTime + speedRotation)
        {
            streetRendu.GetComponent<SpriteRenderer>().color = Color.Lerp(crColor, nxtColor, (Time.time - startTime) / speedRotation);
            yield return null;
        }
    }
}

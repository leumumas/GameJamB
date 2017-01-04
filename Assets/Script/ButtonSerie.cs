using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSerie : MonoBehaviour {
    private List<Button> buttonTable = new List<Button>();
    private int noPlayer;

    public ButtonSerie(int player, int stacks)
    {
        noPlayer = player;
        for (int i = 0; i < stacks; i++)
        {
            buttonTable.Add(new Button());
        }
    }

	void Start () {
        
	}

	void Update () {
		
	}
}

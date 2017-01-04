using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSerie : MonoBehaviour {
    private List<Button> buttonTable = new List<Button>();

    public ButtonSerie(int player, int stacks)
    {
        for (int i = 0; i < stacks; i++)
        {
            buttonTable.Add(new Button(player));
        }
    }

	void Start () {
        
	}

	void Update () {
		
	}
}

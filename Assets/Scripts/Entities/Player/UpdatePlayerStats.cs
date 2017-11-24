using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdatePlayerStats : MonoBehaviour {

    public Entity player;

    private Text nameText, hlthText, manaText, lvlText, stamText;

	// Use this for initialization
	void Start () {
        nameText = transform.GetChild(0).GetComponent<Text>();
        lvlText  = transform.GetChild(1).GetComponent<Text>();
        hlthText = transform.GetChild(2).GetComponent<Text>();
        manaText = transform.GetChild(3).GetComponent<Text>();
        stamText = transform.GetChild(4).GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        nameText.text = "Name: "    + player.Name();
        lvlText.text  = "Level: "   + player.Level().ToString();
        hlthText.text = "Health: "  + player.Health().ToString();
        manaText.text = "  Mana: "    + player.Mana().ToString();
        stamText.text = "Stamina: " + player.Stamina().ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCharacterPanel : MonoBehaviour {

    public Entity player;

    private Text lvlText, nxtLvlText, strText, intText, wepText, headText, chestText, legText, backText;


	// Use this for initialization
	void Start () {
		lvlText =    transform.GetChild(0).GetComponent<Text>();
        nxtLvlText = transform.GetChild(1).GetComponent<Text>();
        strText =    transform.GetChild(2).GetComponent<Text>();
        intText =    transform.GetChild(3).GetComponent<Text>();
        wepText =    transform.GetChild(4).GetComponent<Text>();
        headText =   transform.GetChild(5).GetComponent<Text>();
        chestText =  transform.GetChild(6).GetComponent<Text>(); 
        legText =    transform.GetChild(7).GetComponent<Text>();
        backText =   transform.GetChild(8).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		lvlText.text = "Level: " + player.Level().ToString();
        nxtLvlText.text = "Next Level in: 12345";
        strText.text = "Strength: " + player.Strength();
        intText.text = "Intelligence: " + player.Intel();
        wepText.text = "Weapon: None";
        headText.text = "Head: None";
        chestText.text = "Chest: None";
        legText.text = "Legs: None";
        backText.text = "Back: None";
	}
}

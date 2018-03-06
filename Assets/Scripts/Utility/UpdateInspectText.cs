using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateInspectText : MonoBehaviour {

    private Text nameText, levelText, healthText, manaText, stamText;
    public Entity target;
	// Use this for initialization
	void Start () {
        nameText   = transform.GetChild(0).GetComponent<Text>();
        levelText  = transform.GetChild(1).GetComponent<Text>();
        healthText = transform.GetChild(2).GetComponent<Text>();
        manaText   = transform.GetChild(3).GetComponent<Text>();
        stamText   = transform.GetChild(4).GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            nameText.text = "Name: " + target.Name;
            levelText.text = "Level: " + target.Level().ToString();
            healthText.text = "Health: " + target.Health().ToString();
            manaText.text = "Mana: " + target.Mana().ToString();
            stamText.text = "Stamina: " + target.Stamina().ToString();
        }
	}
}

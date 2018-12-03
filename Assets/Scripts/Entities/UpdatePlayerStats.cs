using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdatePlayerStats : MonoBehaviour {

    public Entity player;

    private Text nameText, lvlText, targetText;
    private Slider healthSlider, manaSlider, staminaSlider;

	// Use this for initialization
	void Start () {
        nameText = transform.Find("PlayerNameText").GetComponent<Text>();
        lvlText  = transform.Find("PlayerLevelText").GetComponent<Text>();
        healthSlider = transform.Find("HealthBar").GetComponent<Slider>();
        manaSlider = transform.Find("ManaBar").GetComponent<Slider>();
        staminaSlider = transform.Find("StaminaBar").GetComponent<Slider>();

    }
	
	// Update is called once per frame
	void Update () {
        nameText.text = player.Name;
        lvlText.text  = player.Level().ToString();
        healthSlider.value  = player.Health() / player.MaxHealth();
        healthSlider.transform.Find("HealthText").GetComponent<Text>().text = player.Health() + "/" + player.MaxHealth();
        manaSlider.value    = player.Mana() / player.MaxMana();
        manaSlider.transform.Find("ManaText").GetComponent<Text>().text = player.Mana() + "/" + player.MaxMana();
        staminaSlider.value = player.Stamina() / player.MaxStamina();
        staminaSlider.transform.Find("StaminaText").GetComponent<Text>().text = player.Stamina() + "/" + player.MaxStamina();
    }
}

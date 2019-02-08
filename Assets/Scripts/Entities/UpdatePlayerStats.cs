using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdatePlayerStats : MonoBehaviour {

    public Entity player;

    public Text nameText, lvlText, manaText, healthText, staminaText;
    public Slider healthSlider, manaSlider, staminaSlider;
    public bool loaded = false;

	
	// Update is called once per frame
	void Update () {
        if (loaded)
        {
        if (player != null)
            nameText.text = player.Name;
        lvlText.text  = player.Level().ToString();
        healthSlider.value  = player.Health() / player.MaxHealth();
        healthText.text = player.Health() + "/" + player.MaxHealth();
        manaSlider.value    = player.Mana() / player.MaxMana();
        manaText.text = player.Mana() + "/" + player.MaxMana();
        staminaSlider.value = player.Stamina() / player.MaxStamina();
        staminaText.text = player.Stamina() + "/" + player.MaxStamina();
        }
    }
}

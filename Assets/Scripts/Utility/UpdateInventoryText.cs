using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateInventoryText : MonoBehaviour {

    public Entity player;

    private Text invText;
	// Use this for initialization
	void Start () {
        invText = transform.Find("InvText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if(player.Inventory().Count <= 0)
            invText.text = "Nothing in your Inventory.";
        else 
            invText.text = "Invnetory:\n" + player.PrintInventory();
	}
}

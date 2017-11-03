using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Room : MonoBehaviour {

    public Exit[] exits;
	public Item[] items;
    public string location = "World", Name = "Place";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CheckExit(string exit){
		bool good = false;
		for (int i = 0; i <= exits.Length - 1; i++) {
			if (exits [i].name.ToLower () == exit.ToLower ()) {
				GameObject.Find ("Player").GetComponent<Movement> ().moving = true;
				GameObject.Find ("Player").GetComponent<Movement> ().newPos = exits [i].transform;
				good = true;
			}
		}
		if (!good) {
			GameObject chat = GameObject.Find ("PlayerControl").gameObject;
			chat.transform.Find ("ScrollView").gameObject.transform.Find ("Viewport").gameObject.transform.Find ("Content").gameObject.GetComponent<Text> ().color = new Color (0, 0, 1);
			chat.GetComponent<UpdateChatText> ().UpdateChat ("<color=#00ffffff>>" + Name + ": Can't go that way!</color>");
			chat.transform.Find ("ScrollView").gameObject.transform.Find ("Viewport").gameObject.transform.Find ("Content").gameObject.GetComponent<Text> ().color = new Color (1, 1, 1);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateRoomStats : MonoBehaviour {

    public Entity player;

	private Text locationText, nameText, exitsText, npcText;
	private Room room;

	// Use this for initialization
	void Start () {
        locationText = transform.Find("RoomLocationText").GetComponent<Text>();
        nameText     = transform.Find("RoomNameText").GetComponent<Text>();
        exitsText    = transform.Find("RoomExitsText").GetComponent<Text>();
		npcText      = transform.Find ("RoomNPCText").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		room = player.Location();
        locationText.text = "Location: " + room.location;
        nameText.text     = "Name: " + room.Name;
		exitsText.text    = PrintExits (room);
		npcText.text      = PrintNPC (room);
	}

	string PrintExits(Room room) {
		string ret = "Exits: \n";
		for (int i = 0; i < room.exits.Length; i++)
			ret = ret + room.exits [i].name + ": " + room.exits [i].goesTo.transform.parent.gameObject.GetComponent<Room> ().Name + "\n";
		return ret;
	}

	string PrintNPC(Room room) {
		string ret = "NPCs: \n";
		foreach (Entity e in room.ents) {
			if(!e.isPlayer)
				ret = ret + e.name + '\n'; 
		}
		return ret;
	}
}

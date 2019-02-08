using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateRoomStats : MonoBehaviour {

    public Entity player;

	public Text areaText, locationText, npcText, itemText;
    public bool loaded = false;
	private Room room;
	
	// Update is called once per frame
	void Update () {
        if (loaded)
        {
		room = player.location;
        areaText.text = "Area: " + room.location;
        locationText.text     = "Location: " + room.Name;
		npcText.text      = PrintNPC (room);
        itemText.text     = PrintItems(room);
        }
	}

	string PrintExits(Room room) {
		string ret = "Exits: \n";
		for (int i = 0; i < room.exits.Count; i++)
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

    string PrintItems(Room room)
    {
        string ret = "Items: \n";
        foreach (Item i in room.items)
        {
            ret = ret + i.name + '\n';
        }
        return ret;
    }
}

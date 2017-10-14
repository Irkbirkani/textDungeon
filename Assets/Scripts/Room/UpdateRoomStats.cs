using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdateRoomStats : MonoBehaviour {

    public Room room;

    private Text locationText, nameText, exitsText;

	// Use this for initialization
	void Start () {
        locationText = transform.Find("RoomLocationText").GetComponent<Text>();
        nameText     = transform.Find("RoomNameText").GetComponent<Text>();
        exitsText    = transform.Find("RoomExitsText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        locationText.text = "Location: " + room.location;
        nameText.text     = "Name: " + room.Name;
        exitsText.text = "Exits: \n    North: " + room.exits[0].goesTo.Name +
            "\n    East: " + room.exits[1].goesTo.Name +
            "\n    South: " + room.exits[2].goesTo.Name +
            "\n    West: " + room.exits[3].goesTo.Name;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ProcessInput : MonoBehaviour {

	public Entity player;
	public InputField input;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//processInput (input.text);
	}

	public void processInput(string input){
		if (input == PlayerCommands.ExitEast)
			player.Location ().CheckExit ("east");
		if (input == PlayerCommands.ExitNorth)
			player.Location ().CheckExit ("north");
		if (input == PlayerCommands.ExitSouth)
			player.Location ().CheckExit ("south");
		if (input == PlayerCommands.ExitWest)
			player.Location ().CheckExit ("west");
	}
}

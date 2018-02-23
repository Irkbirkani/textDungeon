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
		var inp = input.Split (' ');
		if (inp[0] == PlayerCommands.ExitEast)
			player.Location ().CheckExit ("east");
		if (inp[0] == PlayerCommands.ExitNorth)
			player.Location ().CheckExit ("north");
		if (inp[0] == PlayerCommands.ExitSouth)
			player.Location ().CheckExit ("south");
		if (inp[0] == PlayerCommands.ExitWest)
			player.Location ().CheckExit ("west");
		if (inp[0] == PlayerCommands.Attack && inp.Length == 2)
			player.Location().CheckAttack(inp[1]);
	}
}

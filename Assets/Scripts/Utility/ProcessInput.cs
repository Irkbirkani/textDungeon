using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ProcessInput : MonoBehaviour {

	public Entity player;
	public InputField input;

	private GameObject chat;

	// Use this for initialization
	void Start () {
		chat = GameObject.Find ("PlayerControl").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		//processInput (input.text);
	}

	public void processInput(string input){
		var moving = player.GetComponent<Movement>().moving;
		var inp = input.Split (' ');
        if (inp[0] == PlayerCommands.ExitEast && !moving && !player.attacking)
            player.Location().CheckExit("east");
        else if (inp[0] == PlayerCommands.ExitNorth && !moving && !player.attacking)
            player.Location().CheckExit("north");
        else if (inp[0] == PlayerCommands.ExitSouth && !moving && !player.attacking)
            player.Location().CheckExit("south");
        else if (inp[0] == PlayerCommands.ExitWest && !moving && !player.attacking)
            player.Location().CheckExit("west");
        else if (inp[0] == PlayerCommands.Attack && inp.Length == 2 && !player.attacking)
            player.Location().CheckAttack(inp[1]);
        else if (inp[0] == PlayerCommands.Inspect && inp.Length == 2 && !player.attacking && !moving)
            player.Location().CheckInspect(inp[1]);
        else if (inp[0] == PlayerCommands.Target && inp.Length == 2 && !player.attacking && !moving)
            player.Location().CheckTarget(inp[1]);
        else if (inp[0] == PlayerCommands.Rest && !moving)
        {
            player.resting = true;
            chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(0, 0, 1);
            chat.GetComponent<UpdateChatText>().UpdateChat("<color=#00ffffff>> Resting. </color>");
            chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(1, 1, 1);
            chat.transform.Find("ScrollView").gameObject.GetComponent<ScrollRect>().gameObject.transform.Find("Scrollbar Vertical").gameObject.GetComponent<Scrollbar>().value = 0.0f;
        }
        else if (inp[0] == PlayerCommands.Flee && player.attacking)
        {
            var arr = new string[] { "north", "south", "east", "west" };
            player.Location().CheckExit(arr[Random.Range(0, 3)]);
        }
        else
        {
            chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(0, 0, 1);
            chat.GetComponent<UpdateChatText>().UpdateChat("<color=#00ffffff>> What? </color>");
            chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(1, 1, 1);
            chat.transform.Find("ScrollView").gameObject.GetComponent<ScrollRect>().gameObject.transform.Find("Scrollbar Vertical").gameObject.GetComponent<Scrollbar>().value = 0.0f;
        }
	}
}

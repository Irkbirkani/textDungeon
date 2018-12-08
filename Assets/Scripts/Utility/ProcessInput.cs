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
        if (inp[0].Equals(PlayerCommands.ExitEast) && !moving && !player.attacking)
            player.Location().Check("", "move", "east");
        else if (inp[0].Equals(PlayerCommands.ExitNorth) && !moving && !player.attacking)
            player.Location().Check("", "move", "north");
        else if (inp[0].Equals(PlayerCommands.ExitSouth) && !moving && !player.attacking)
            player.Location().Check("", "move", "south");
        else if (inp[0].Equals(PlayerCommands.ExitWest) && !moving && !player.attacking)
            player.Location().Check("", "move", "west");
        else if (inp[0].Equals(PlayerCommands.Step) && !moving)
        {
            if (inp.Length == 3)
                player.Location().Check(inp[1] + " " + inp[2], "move", "step");
            else
                player.Location().Check(inp[1] + " 1", "move", "step");
        }
        else if (inp[0].Equals(PlayerCommands.Attack) && inp.Length == 2 && !player.attacking)
            player.Location().Check(inp[1], "ent", "attack");
        else if (inp[0].Equals(PlayerCommands.Inspect) && inp.Length == 2 && !player.attacking && !moving)
            player.Location().Check(inp[1], "ent", "inspect");
        else if (inp[0].Equals(PlayerCommands.Target) && inp.Length == 2 && !player.attacking && !moving)
            player.Location().Check(inp[1], "ent", "target");
        else if (inp[0].Equals(PlayerCommands.Get) && inp.Length == 2 && !player.attacking && !moving)
            player.Location().Check(inp[1], "item", "get");
        else if (inp[0].Equals(PlayerCommands.Drop) && inp.Length == 2 && !player.attacking && !moving)
            player.DropItem(inp[1]);
        else if (inp[0].Equals(PlayerCommands.Character))
        {
            GameObject info = GameObject.Find("InfoPanel").gameObject;
            var child = info.transform.Find("CharacterPanel");
            info.GetComponent<InfoPanel>().Deactivate();
            child.gameObject.SetActive(true);
        }
        else if (inp[0].Equals(PlayerCommands.Inventory))
        {
            GameObject info = GameObject.Find("InfoPanel").gameObject;
            var child = info.transform.Find("InventoryPanel");
            info.GetComponent<InfoPanel>().Deactivate();
            child.gameObject.SetActive(true);
        }
        else if (inp[0].Equals(PlayerCommands.Rest) && !moving && !player.attacking)
        {
            player.resting = true;
            chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(0, 0, 1);
            chat.GetComponent<UpdateChatText>().UpdateChat("<color=#00ffffff>> Resting. </color>");
            chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(1, 1, 1);
            chat.transform.Find("ScrollView").gameObject.GetComponent<ScrollRect>().gameObject.transform.Find("Scrollbar Vertical").gameObject.GetComponent<Scrollbar>().value = 0.0f;
        }
        else if (inp[0].Equals(PlayerCommands.Flee) && player.attacking)
        {
            var arr = new string[] { "north", "south", "east", "west" };
            player.attacking = false;
            player.Location().Check("", "move", arr[Random.Range(0, 3)]);
        }
        else
        {
            player.Location().print("< What?", "#00ffffff");
        }
	}
}

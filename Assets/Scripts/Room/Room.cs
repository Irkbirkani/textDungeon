using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Room : MonoBehaviour {

    public Exit[] exits;
	public List<Item> items;
	public List<Entity> ents = new List<Entity>();
    public string location = "World", Name = "Place";
	public Vector2 startLoc;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CheckAttack(string tgt) {
		
	}
    
    public void AddEntity(Entity ent)
    {
        ents.Add(ent);
        ent.transform.parent = transform;
    }

	public Entity GetPlayer() {
		Entity pl = null;
		foreach (Entity e in ents) {
			if (e.isPlayer) {
				pl = e;
			}
		}
		return pl;
	}

    public void RemoveEntity(Entity ent)
    {
        ents.Remove(ent);
    }

	public void CheckExit(string exit){
		bool good = false;
		for (int i = 0; i <= exits.Length - 1; i++) {
			if (exits [i].name.ToLower () == exit.ToLower ()) {
				var pl = GetPlayer ();
				pl.GetComponent<Movement> ().SetMove(true, exits [i].transform);
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

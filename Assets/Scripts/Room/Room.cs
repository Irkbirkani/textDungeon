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
    public int distance = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CheckAttack(string tgt) {
		foreach(Entity e in ents)
        {
            if (e.Name.ToLower().Equals(tgt.ToLower()))
            {
                Entity pl = GetPlayer();
                pl.resting = false;
				pl.GetComponent<Movement> ().SetMove (true, e.transform, 1.0f);
                GameObject chat = GameObject.Find("PlayerControl").gameObject;
                chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(0, 0, 1);
                chat.GetComponent<UpdateChatText>().UpdateChat("<color=#ff0000ff>> Attacking "+e.Name+"!</color>");
                chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(1, 1, 1);
                chat.transform.Find("ScrollView").gameObject.GetComponent<ScrollRect>().gameObject.transform.Find("Scrollbar Vertical").gameObject.GetComponent<Scrollbar>().value = 0.0f;
                pl.Attack(e);
            }
        }
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
                pl.resting = false;
				pl.GetComponent<Movement> ().SetMove(true, exits [i].transform, 0.01f);
                pl.SetStamina(-distance);
				good = true;
			}
		}
		if (!good) {
			GameObject chat = GameObject.Find ("PlayerControl").gameObject;
			chat.transform.Find ("ScrollView").gameObject.transform.Find ("Viewport").gameObject.transform.Find ("Content").gameObject.GetComponent<Text> ().color = new Color (0, 0, 1);
			chat.GetComponent<UpdateChatText> ().UpdateChat ("<color=#00ffffff>>"+ " " + Name + ": Can't go that way!</color>");
			chat.transform.Find ("ScrollView").gameObject.transform.Find ("Viewport").gameObject.transform.Find ("Content").gameObject.GetComponent<Text> ().color = new Color (1, 1, 1);
			chat.transform.Find ("ScrollView").gameObject.GetComponent<ScrollRect> ().gameObject.transform.Find ("Scrollbar Vertical").gameObject.GetComponent<Scrollbar> ().value = 0.0f;
		}
	}
}

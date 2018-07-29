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

    public void Check(string tgt, string type, string cmd)
    {
        switch (type)
        {
            case "ent": 
                foreach (Entity e in ents)
                {
                    if (e.Name.ToLower().Equals(tgt.ToLower()))
                    {
                        switch (cmd)
                        {
                            case "target":
                                Target(e);
                                return;
                            case "inspect":
                                Inspect(e);
                                return;
                            case "attack":
                                Attack(e);
                                return;
                        }
                    }
                }
                return;
            case "item":
                foreach (Item i in items)
                {
                    if (i.Name.ToLower().Equals(tgt.ToLower()))
                    {
                        switch (cmd)
                        {
                            case "get":
                                GetItem(i);
                                return;
                        }
                    }
                }
                return;
            case "move":
                switch (cmd)
                {
                    case "east":
                        Exit(cmd);
                        return;
                    case "west":
                        Exit(cmd);
                        return;
                    case "south":
                        Exit(cmd);
                        return;
                    case "north":
                        Exit(cmd);
                        return;
                }
                return;
        }
    }

    void Target(Entity tgt)
    {
        var pl = GetPlayer();
        pl.SetTarget(tgt);
    }

    void Inspect(Entity tgt)
    {
        GameObject info = GameObject.Find("InfoPanel").gameObject;
        info.GetComponent<ActivateChild>().Activate(2);
        info.GetComponent<ActivateChild>().GetActiveChild().GetComponent<UpdateInspectText>().target = tgt;
        var pl = GetPlayer();
        pl.SetTarget(tgt);
    }

    void GetItem(Item tgt)
    {
        var pl = GetPlayer();
        if (pl.AddToInventory(tgt))
        {
            pl.GetComponent<Movement>().SetMove(true, tgt.transform, 1.0f);
            items.Remove(tgt);
            print("< " + tgt.Name + " added to inventory", "#00ffffff");
            tgt.gameObject.SetActive(false);
            pl.GetComponent<Movement>().SetMove(true, transform, 0.01f);
        }
    }

    public bool InRoom(string tgt)
    {
        foreach(Entity e in ents)
        {
            if (e.Name.ToLower().Equals(tgt.ToLower()))
                return true;
        }
        return false;
    }

	void Attack(Entity tgt) {
           Entity pl = GetPlayer();
           pl.resting = false;
		   pl.GetComponent<Movement> ().SetMove (true, tgt.transform, 1.0f);
           print("< Attacking " + tgt.Name + "!", "#ff0000ff");
           pl.Attack(tgt);
	}

    public void print(string s, string col)
    {
        GameObject chat = GameObject.Find("PlayerControl").gameObject;
        chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(0, 0, 1);
        chat.GetComponent<UpdateChatText>().UpdateChat("<color="+col+"> " + s + "</color>");
        chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(1, 1, 1);
        chat.transform.Find("ScrollView").gameObject.GetComponent<ScrollRect>().gameObject.transform.Find("Scrollbar Vertical").gameObject.GetComponent<Scrollbar>().value = 0.0f;
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

	void Exit(string exit){
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
            print("< "+ Name + ": Can't go that way!", "#00ffffff");
		}
	}
}

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

    public void CheckTarget(string tgt)
    {
        foreach (Entity e in ents)
        {
            if (e.Name.ToLower().Equals(tgt.ToLower()))
            {
                var pl = GetPlayer();
                pl.SetTarget(e);
            }

        }

    }

    public void CheckInspect(string tgt)
    {
        foreach (Entity e in ents)
        {
            if (e.Name.ToLower().Equals(tgt.ToLower()))
            {
                GameObject info = GameObject.Find("InfoPanel").gameObject;
                info.GetComponent<ActivateChild>().Activate(2);
                info.GetComponent<ActivateChild>().GetActiveChild().GetComponent<UpdateInspectText>().target = e;
                var pl = GetPlayer();
                pl.SetTarget(e);
            }
                
        }
        
    }

    public void CheckItem(string tgt)
    {
        foreach (Item i in items)
        {
            if (i.Name.ToLower().Equals(tgt.ToLower()))
            {
                Debug.Log("Here");
                var pl = GetPlayer();
                if (pl.AddToInventory(i))
                {
                    pl.GetComponent<Movement>().SetMove(true, i.transform, 1.0f);
                    items.Remove(i);
                    print(i.Name + " added to inventory", "#00ffffff");
                    i.gameObject.SetActive(false);
                    pl.GetComponent<Movement>().SetMove(true, transform, 0.01f);
                    break;
                }
            }

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

	public void CheckAttack(string tgt) {
		foreach(Entity e in ents)
        {
            if (e.Name.ToLower().Equals(tgt.ToLower()))
            {
                Entity pl = GetPlayer();
                pl.resting = false;
				pl.GetComponent<Movement> ().SetMove (true, e.transform, 1.0f);
                print("Attacking " + e.Name + "!", "#ff0000ff");
                pl.Attack(e);
            }
        }
	}

    private void print(string s, string col)
    {
        GameObject chat = GameObject.Find("PlayerControl").gameObject;
        chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(0, 0, 1);
        chat.GetComponent<UpdateChatText>().UpdateChat("<color="+col+">> " + s + "</color>");
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
            print(Name + ": Can't go that way!", "#00ffffff");
		}
	}
}

using System.Collections;
using System.Xml;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Room : MonoBehaviour {

    public List<Exit> exits;
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

    void Inspect(Object tgt)
    {
        var pl = GetPlayer();
        if (tgt is Entity)
        {
            GameObject info = GameObject.Find("InfoPanel").gameObject;
            info.GetComponent<InfoPanel>().Deactivate();
            var child = info.transform.Find("InspectPanel").gameObject;
            child.SetActive(true);
            child.GetComponent<UpdateInspectText>().target = (Entity)tgt;
            pl.SetTarget((Entity)tgt);
        } else if (tgt is Item && pl.InInventory(tgt.name))
        {

        }    
    }

    void GetItem(Item tgt)
    {
        var pl = GetPlayer();
        if (pl.AddToInventory(tgt))
        {
            pl.GetComponent<Movement>().moveToTarget(tgt.transform, 1.0f, tgt);
        }
    }
    public void AddItem(Item itm)
    {
        items.Add(itm);
        var pl = GetPlayer();
        itm.gameObject.transform.position = new Vector2(pl.transform.position.x + 0.75f, pl.transform.position.y - 0.75f);
        itm.gameObject.SetActive(true);
        print("< " + itm.name + " removed from inventory.", "#00ffffff");
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
		   pl.GetComponent<Movement> ().moveToTarget(tgt.transform, 1.0f, tgt);
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

    public void SortMoveObject(Object obj)
    {
        if(obj is Item)
        {
            Item tgt = (Item)obj;
            items.Remove(tgt);
            print("< " + tgt.Name + " added to inventory", "#00ffffff");
            tgt.gameObject.SetActive(false);
            GetPlayer().GetComponent<Movement>().SetMove(true, transform, 0.01f);
        } else if (obj is Entity)
        {
            Entity tgt = (Entity)obj;
            if (!tgt.IsPlayer())
            {
                print("< Attacking " + tgt.Name + "!", "#ff0000ff");
                GetPlayer().Attack(tgt);
            }
        }
    }

    public void RemoveEntity(Entity ent)
    {
        ents.Remove(ent);
    }

	void Exit(string exit){
		bool good = false;
		for (int i = 0; i <= exits.Count - 1; i++) {
			if (exits [i].name.ToLower ().Equals(exit.ToLower ())) {
				var pl = GetPlayer ();
                if(pl.Target() != null)
                {
                    pl.SetTarget(null);
                    pl.attacking = false;
                }
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

    public void MakeRoom(XmlNode roomNode)
    {
        location = transform.parent.gameObject.name;
        Name = roomNode.Attributes["name"].InnerText;
        int height = System.Convert.ToInt32(roomNode.SelectSingleNode("height").InnerText);
        int width = System.Convert.ToInt32(roomNode.SelectSingleNode("width").InnerText);
        for(int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                GameObject tile = Instantiate(Resources.Load("Prefabs/Tile", typeof(GameObject))) as GameObject;
                tile.transform.parent = this.transform;
                var scale = tile.transform.localScale;
                scale.Set(1f, 1f, 1f);
                tile.transform.localScale = scale;
                tile.transform.localPosition = new Vector2(x - width / 2, y - height / 2);
            }
        }

        
        XmlNode north = roomNode.SelectSingleNode("north");
        XmlNode south = roomNode.SelectSingleNode("south");
        XmlNode east  = roomNode.SelectSingleNode("east");
        XmlNode west  = roomNode.SelectSingleNode("west");
        CreateExit(north, new Vector2(0, 3));
        CreateExit(south, new Vector2(0, -(height / 2)-1));
        CreateExit(east, new Vector2((width/2)+1, 0));
        CreateExit(west, new Vector2(-(width/2)-1, 0));

        

       // for(int i = 4; i < 12; i+=2)
       // {
       //     if (param[i].Equals("1"))
       //     {
       //         GameObject exit = Instantiate(Resources.Load("Prefabs/Exit", typeof(GameObject))) as GameObject;
       //         exit.transform.parent = this.transform;
       //         switch (i){
       //             case 4:
       //                 exit.GetComponent<Exit>().MakeExit("north", param[i + 1], new Vector2(0, (System.Convert.ToInt32(param[2]) / 2) + 1));
       //                 exits.Add(exit.GetComponent<Exit>());
       //                 break;
       //             case 6:
       //                 exit.GetComponent<Exit>().MakeExit("east", param[i + 1], new Vector2((System.Convert.ToInt32(param[3]) / 2) + 1, 0));
       //                 exits.Add(exit.GetComponent<Exit>());
       //                 break;
       //             case 8:
       //                 exit.GetComponent<Exit>().MakeExit("south", param[i + 1], new Vector2(0, -(System.Convert.ToInt32(param[2]) / 2) - 1));
       //                 exits.Add(exit.GetComponent<Exit>());
       //                 break;
       //             case 10:
       //                 exit.GetComponent<Exit>().MakeExit("west", param[i + 1], new Vector2(-(System.Convert.ToInt32(param[2]) / 2) - 1, 0));
       //                 exits.Add(exit.GetComponent<Exit>());
       //                 break;
       //         }
       //     }
       // }        

        distance = System.Convert.ToInt32(roomNode.SelectSingleNode("distance").InnerText);
    }

    void CreateExit(XmlNode xit, Vector2 pos)
    {
        if (xit != null)
        {
            GameObject exit = Instantiate(Resources.Load("Prefabs/Exit", typeof(GameObject))) as GameObject;
            exit.transform.parent = transform;
            exit.GetComponent<Exit>().MakeExit(xit.Name, xit.InnerText, pos);
            exits.Add(exit.GetComponent<Exit>());
        }
    }
}

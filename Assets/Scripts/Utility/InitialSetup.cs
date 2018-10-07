using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Xml;
using UnityEngine;

public class InitialSetup : MonoBehaviour {
    public InputField input;
	public Room startRoom;

    private bool testing = true;


    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        if(testing)
            LoadLocation("/Resources/Data/LocationData/TestWorld");
        input.Select();
        input.ActivateInputField();

       //startRoom.AddEntity(GameObject.Find("Player").GetComponent<Entity>());
       //startRoom.gameObject.SetActive(true);
       //GameObject.Find ("Player").GetComponent<Entity>().SetLocation(startRoom);
    }

    void LoadLocation(string locationPath)
    {
        GameObject loc = new GameObject(locationPath);
        loc.transform.position = new Vector2(0, 0);

        XmlDocument locFile = new XmlDocument();
        locFile.Load(Application.dataPath + locationPath + "/Rooms.xml");
        foreach(XmlNode locNode in locFile)
        {
             string locationName =  locNode.Name;
             foreach (XmlNode roomNode in locNode)
            {
                CreateRooms(roomNode, loc);
            }
        }
















        //var Roomfile = Resources.Load<TextAsset>(locationPath + "/RoomData");
        //var Itemfile = Resources.Load<TextAsset>(locationPath + "/ItemData");
        //var Enemyfile = Resources.Load<TextAsset>(locationPath + "/EnemyData");
        //CreateRooms(Roomfile, loc);
        //LoadEnemies(Enemyfile, loc);
        //LoadItems(Itemfile);
        //LoadPlayer(??);
    }

    void CreateRooms(XmlNode roomNode, GameObject loc)
    {
        GameObject room;
        room = Instantiate(Resources.Load("Prefabs/Room", typeof(GameObject))) as GameObject;
        room.transform.parent = loc.transform;
        room.gameObject.name = roomNode.Attributes["name"].InnerText;
        //the next few lines check and see if a room does not have a north exit and does have a south exit.
        //this is to set the rooms in the correct position visually when they are loaded.
        bool north = true, south = false;
        foreach (XmlNode chldNode in roomNode.ChildNodes)
        {
            if (chldNode.Name.Equals("north")) north = false;
            if (chldNode.Name.Equals("south")) south = true;
        }
        room.transform.position = north && south ? new Vector2(0, 2.75f) : new Vector2(0, 2);
        room.transform.localScale-= new Vector3(0.1f, 0.2f, 0);
        room.GetComponent<Room>().MakeRoom(roomNode);
        room.gameObject.SetActive(false);
        //LinkExits(loc);
    }

    void LinkExits(GameObject loc)
    {
        for(int i = 0; i <= loc.transform.childCount-1; ++i)
        {
            List<Exit> exits = loc.transform.GetChild(i).GetComponent<Room>().exits;
            foreach(Exit e in exits)
            {
                switch (e.name.ToLower()) {
                    case "north":
                        e.goesTo = loc.transform.Find(e.exitTo).GetComponent<Room>().transform.Find("south").GetComponent<Exit>();
                        break;
                    case "south":
                        e.goesTo = loc.transform.Find(e.exitTo).GetComponent<Room>().transform.Find("north").GetComponent<Exit>();
                        break;
                    case "west":
                        e.goesTo = loc.transform.Find(e.exitTo).GetComponent<Room>().transform.Find("east").GetComponent<Exit>();
                        break;
                    case "east":
                        e.goesTo = loc.transform.Find(e.exitTo).GetComponent<Room>().transform.Find("west").GetComponent<Exit>();
                        break;
                }
            }
        }
    }

    void LoadEnemies(TextAsset enemies, GameObject loc)
    {
        string[] lines = enemies.text.Split('\n');
        GameObject enemy;
        foreach(string s in lines)
        {
            enemy = Instantiate(Resources.Load("Prefabs/Enemy", typeof(GameObject))) as GameObject;
            string[] paramaters = s.Split(',');
            enemy.transform.parent = loc.transform.Find(paramaters[1]);
            enemy.GetComponent<Entity>().MakeEntity(paramaters, false);
        }
    }

    void LoadNPCS()
    {

    }

    void LoadItems(TextAsset items)
    {

    }

    void LoadPlayer(TextAsset player)
    {

    }

}

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
        loc.name = locationPath.Split('/')[locationPath.Split('/').Length-1];

        XmlDocument locFile = new XmlDocument();
        locFile.Load(Application.dataPath + locationPath + "/Rooms.xml");
        foreach(XmlNode locNode in locFile)
        {
            string locationName =  locNode.Name;
            foreach (XmlNode roomNode in locNode)
            {
                CreateRooms(roomNode, loc);
            }
            foreach (XmlNode roomNode in locNode)
        
            {
                LinkExits(roomNode, loc);
            }
        }
        if (testing)
        {
            loc.transform.Find("Place").gameObject.SetActive(true);
            loc.transform.Find("Place").GetComponent<Room>().AddEntity(GameObject.Find("Player").GetComponent<Entity>());
            GameObject.Find("Player").GetComponent<Entity>().SetLocation(loc.transform.Find("Place").GetComponent<Room>());
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
        string roomType = roomNode.SelectSingleNode("type").InnerText;
        room = Instantiate(Resources.Load("Prefabs/Rooms/"+roomType, typeof(GameObject))) as GameObject;
        room.transform.parent = loc.transform;
        room.gameObject.name = roomNode.Attributes["name"].InnerText;
        room.GetComponent<Room>().distance = System.Int32.Parse(roomNode.SelectSingleNode("distance").InnerText);
        room.gameObject.SetActive(false);
        LoadEnemies(roomNode, loc);
    }

    void LinkExits(XmlNode roomNode, GameObject loc)
    {
        Room curRoom = loc.transform.Find(roomNode.Attributes["name"].InnerText).GetComponent<Room>();
        
        foreach(XmlNode exit in roomNode)
        {
            if (exit.Name.Equals("exit"))
            {
                Exit ex, dest;
                switch (exit.Attributes["name"].InnerText)
                {
                    case "north":
                        ex = curRoom.exits.Find(x => x.name.Equals("north"));
                        dest = loc.transform.Find(exit.SelectSingleNode("dest").InnerText).GetComponent<Room>().exits.Find(x => x.name.Equals("south"));
                        ex.goesTo = dest;
                        break;
                    case "east":
                        ex = curRoom.exits.Find(x => x.name.Equals("east"));
                        dest = loc.transform.Find(exit.SelectSingleNode("dest").InnerText).GetComponent<Room>().exits.Find(x => x.name.Equals("west"));
                        ex.goesTo = dest;
                        break;
                    case "south":
                        ex = curRoom.exits.Find(x => x.name.Equals("south"));
                        dest = loc.transform.Find(exit.SelectSingleNode("dest").InnerText).GetComponent<Room>().exits.Find(x => x.name.Equals("north"));
                        ex.goesTo = dest;
                        break;
                    case "west":
                        ex = curRoom.exits.Find(x => x.name.Equals("west"));
                        dest = loc.transform.Find(exit.SelectSingleNode("dest").InnerText).GetComponent<Room>().exits.Find(x => x.name.Equals("east"));
                        ex.goesTo = dest;
                        break;
                }
            }
        }
    }

    void LoadEnemies(XmlNode roomNode, GameObject loc)
    {
        GameObject curRoom = loc.transform.Find(roomNode.Attributes["name"].InnerText).gameObject;
        foreach(XmlNode node in roomNode)
        {
            if (node.Name.Equals("Enemy"))
            {
                GameObject enemy = Instantiate(Resources.Load("Prefabs/Enemy", typeof(GameObject))) as GameObject;
                enemy.transform.parent = curRoom.transform;
                enemy.GetComponent<Entity>().location = curRoom.GetComponent<Room>();
                enemy.GetComponent<Entity>().MakeEntity(node, false);
                curRoom.GetComponent<Room>().AddEntity(enemy.GetComponent<Entity>());
            }
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

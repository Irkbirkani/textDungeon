using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Xml;
using UnityEngine;

public class InitialSetup : MonoBehaviour
{
    public InputField input;
    public Entity playerRef;
    public Room startRoom;
    public Canvas canvas;
    public UpdatePlayerStats playerStatsText;
    public UpdateRoomStats roomStats;

    const string PLAYER_PATH = "/Resources/Data/PlayerData/Player.xml";

    //private bool testing = true;

    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        input.ActivateInputField();
        LoadPlayer();
    }

    void LoadLocation(string locationPath)
    {
        GameObject loc = new GameObject();
        loc.transform.position = new Vector2(0, 0);
        loc.name = locationPath.Split('/')[locationPath.Split('/').Length - 1];

        XmlDocument locFile = new XmlDocument();
        locFile.Load(Application.dataPath + locationPath + "/Rooms.xml");
        foreach (XmlNode locNode in locFile)
        {
            string locationName = locNode.Name;
            foreach (XmlNode roomNode in locNode)
            {
                CreateRooms(roomNode, loc);
            }
            foreach (XmlNode roomNode in locNode)

            {
                LinkExits(roomNode, loc);
            }
        }
    }

    void CreateRooms(XmlNode roomNode, GameObject loc)
    {
        GameObject room;
        string roomType = roomNode.SelectSingleNode("type").InnerText;
        room = Instantiate(Resources.Load("Prefabs/Rooms/" + roomType, typeof(GameObject))) as GameObject;
        room.transform.parent = loc.transform;
        room.gameObject.name = roomNode.Attributes["name"].InnerText;
        room.GetComponent<Room>().location = loc.name;
        room.GetComponent<Room>().Name = room.name;
        room.GetComponent<Room>().distance = System.Int32.Parse(roomNode.SelectSingleNode("distance").InnerText);
        room.gameObject.SetActive(false);
        LoadEnemies(roomNode, loc);
    }

    void LinkExits(XmlNode roomNode, GameObject loc)
    {
        Room curRoom = loc.transform.Find(roomNode.Attributes["name"].InnerText).GetComponent<Room>();

        foreach (XmlNode exit in roomNode)
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
        foreach (XmlNode node in roomNode)
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

    void LoadPlayer()
    {
        XmlDocument PlayerFile = new XmlDocument();
        PlayerFile.Load(Application.dataPath + PLAYER_PATH);
        XmlNode PlayerNode = PlayerFile.SelectSingleNode("player");
        XmlNode location = PlayerNode.SelectSingleNode("location");
        XmlNode world = location.SelectSingleNode("world");
        LoadLocation("/Resources/Data/LocationData/" + world.InnerText);
        Entity player = Instantiate(playerRef);
        GameObject room = GameObject.Find(world.InnerText).transform.Find(location.SelectSingleNode("room").InnerText).gameObject;
        canvas.GetComponent<CanvasController>().SetPlayer(player);
        player.transform.parent = room.transform;
        player.location = room.GetComponent<Room>();
        playerStatsText.player = player;
        roomStats.player = player;
        player.TargetPanel = playerStatsText;
        player.MakeEntity(PlayerNode.SelectSingleNode("stats"), true);
        room.GetComponent<Room>().AddEntity(player);
        room.SetActive(true);
        roomStats.loaded = true;
        playerStatsText.loaded = true;
        //TODO: Add Inventory and Gear loading
    }
}

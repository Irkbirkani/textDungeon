using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class InitialSetup : MonoBehaviour {
    public InputField input;
	public Room startRoom;

    private bool testing = false;

    private void Awake()
    {
        if(testing)
            LoadLocation("Data/LocationData/TestWorldData");
    }

    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        input.Select();
        input.ActivateInputField();

        startRoom.AddEntity(GameObject.Find("Player").GetComponent<Entity>());

        startRoom.gameObject.SetActive(true);
        GameObject.Find ("Player").GetComponent<Entity>().SetLocation(startRoom);
    }

    void LoadLocation(string locationPath)
    {
        GameObject loc = new GameObject(locationPath);
        loc.transform.position = new Vector2(0, 0);
        var Roomfile = Resources.Load<TextAsset>(locationPath + "RoomData");
        //var Itemfile = Resources.Load<TextAsset>(locationPath + "ItemData");
        //var Enemyfile = Resources.Load<TextAsset>(locationPath + "EnemyData");
        CreateRooms(Roomfile, loc);
        //PlaceEnemies(Enemyfile);
        //PlaceItems(Itemfile);
        //LoadPlayer(??);
    }

    void CreateRooms(TextAsset rooms, GameObject loc)
    {
        string[] lines = rooms.text.Split('\n');
        foreach(string s in lines)
        {
            string[] paramaters = s.Split(',');
            var room = Instantiate(Resources.Load("Room", typeof(GameObject))) as GameObject;
            room.transform.parent = loc.transform;
            room.GetComponent<Room>().MakeRoom(paramaters);
            room.gameObject.SetActive(false);
        }
        LinkExits(loc);
    }

    void PlaceEnemies(TextAsset enemy)
    {

    }

    void PlaceNPCS()
    {

    }

    void PlaceItems(TextAsset items)
    {

    }

    void LoadPlayer(TextAsset player)
    {

    }

    void LinkExits(GameObject loc)
    {
        for(int i = 0; i <= loc.transform.childCount; ++i)
        {
            var exits = loc.transform.GetChild(i).GetComponent<Room>().exits;
            foreach(Exit e in exits)
            {
                switch (e.name.ToLower()) {
                    case "north":
                        e.goesTo = loc.transform.Find(e.exitTo).GetComponent<Room>().transform.Find("south").GetComponent<Exit>();
                        return;
                    case "south":
                        e.goesTo = loc.transform.Find(e.exitTo).GetComponent<Room>().transform.Find("north").GetComponent<Exit>();
                        return;
                    case "west":
                        e.goesTo = loc.transform.Find(e.exitTo).GetComponent<Room>().transform.Find("east").GetComponent<Exit>();
                        return;
                    case "east":
                        e.goesTo = loc.transform.Find(e.exitTo).GetComponent<Room>().transform.Find("west").GetComponent<Exit>();
                        return;
                }
            }
        }
    }
}

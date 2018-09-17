using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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
            LoadLocation("Data/LocationData/TestWorldData");
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
        var Roomfile = Resources.Load<TextAsset>(locationPath + "/RoomData");
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
        GameObject room;
        foreach(string s in lines)
        {
            room = Instantiate(Resources.Load("Prefabs/Room", typeof(GameObject))) as GameObject;
            string[] paramaters = s.Split(',');
            room.transform.parent = loc.transform;
            room.gameObject.name = paramaters[1];
            room.transform.position = paramaters[4].Equals("0") && paramaters[8].Equals("1") ? new Vector2(0, 2.75f) : new Vector2(0, 2);
            room.transform.localScale-= new Vector3(0.1f, 0.2f, 0);
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
        for(int i = 0; i <= loc.transform.childCount-1; ++i)
        {
            var exits = loc.transform.GetChild(i).GetComponent<Room>().exits;
            foreach(Exit e in exits)
            {
                Debug.Log(e.name);
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

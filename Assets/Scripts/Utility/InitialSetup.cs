using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class InitialSetup : MonoBehaviour {
    public InputField input;
	public Room startRoom;
	
    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        input.Select();
        input.ActivateInputField();

        startRoom.AddEntity(GameObject.Find("Player").GetComponent<Entity>());

        GameObject.Find ("Player").GetComponent<Entity>().SetLocation(startRoom);
    }

    void LoadLocation(string locationPath)
    {
        var Roomfile = Resources.Load<TextAsset>(locationPath + "RoomData");
        var Itemfile = Resources.Load<TextAsset>(locationPath + "ItemData");
        var Enemyfile = Resources.Load<TextAsset>(locationPath + "EnemyData");
        CreateRooms(Roomfile);
        PlaceEnemies(Enemyfile);
        PlaceItems(Itemfile);
    }

    void CreateRooms(TextAsset rooms)
    {
        string[] lines = rooms.text.Split('\n');
        foreach(string s in lines)
        {
            string[] paramaters = s.Split(',');
            var room = Instantiate(Resources.Load("Room", typeof(GameObject))) as GameObject;
        }
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
}

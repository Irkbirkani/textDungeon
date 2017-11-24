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
        GameObject.Find("Player").GetComponent<Entity>().SetPlayer();
    }
}

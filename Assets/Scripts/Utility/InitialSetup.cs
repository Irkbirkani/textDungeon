using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class InitialSetup : MonoBehaviour {
    public InputField input;
	
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        input.Select();
        input.ActivateInputField();
    }
}

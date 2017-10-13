using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public Exit[] exits;
	public Item[] items;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CheckExit(string exit){
		for (int i = 0; i <= exits.Length - 1; i++) {
			if (exits [i].name.ToLower () == exit.ToLower ()) {
				GameObject.Find ("Player").GetComponent<PlayerMovement> ().moveTo (exits [i].transform);
			}
		}
	}
}

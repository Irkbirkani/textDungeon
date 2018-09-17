using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

	public string name;
    public string exitTo;
    public Exit goesTo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public Transform GetNewRoom(){
		return goesTo.transform.parent.transform;
	}
	public GameObject GetParent(){
		return transform.parent.gameObject;
	}

}

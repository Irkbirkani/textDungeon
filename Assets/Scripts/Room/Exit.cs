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

    public void MakeExit(string name, string exit, Vector2 loc)
    {
        this.transform.localPosition = loc;
        this.GetComponent<Exit>().name = name;
        this.gameObject.name = name;
        this.GetComponent<Exit>().exitTo = exit;
        var scale = this.transform.localScale;
        scale.Set(1f, 1f, 1f);
        this.transform.localScale = scale;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour {

    public Transform up, down, left, right;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Transform GetSide(string side)
    {
        switch (side)
        {
            case "up":
                return up;
            case "down":
                return down;
            case "left":
                return left;
            case "right":
                return right;
            default:
                return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Deactivate()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            var child = transform.GetChild(i).gameObject;
            if (child != null)
            {
                child.SetActive(false);
            }
        }
    }
}

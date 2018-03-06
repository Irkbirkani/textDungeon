using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChild : MonoBehaviour {

    private int activeChild = 0;
    private Dictionary<int,GameObject> map = new Dictionary<int, GameObject>();
    

	// Use this for initialization
	void Start () {
        map.Add(0, null);
        map.Add(1, this.transform.GetChild(0).gameObject);
        map.Add(2, this.transform.GetChild(1).gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Activate(int chld)
    {
        if(activeChild >0)
            map[activeChild].SetActive(false);
        map[chld].SetActive(true);
        activeChild = chld;
        
    }

    public GameObject GetActiveChild()
    {
        return map[activeChild];
    }
}

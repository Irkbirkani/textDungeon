using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour {

    public float value, maxValue;

    private float size;

	// Use this for initialization
	void Start () {
        size = this.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(size + " * (" + value + " / " + maxValue + ") = " + size * (value / maxValue));
        this.transform.localScale = new Vector3(size * (value / maxValue), this.transform.localScale.y, this.transform.localScale.z);
	}
}

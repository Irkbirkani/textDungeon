using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void moveTo(Transform newPos){
		Vector2 vel = GetComponent<Transform> ().position - newPos.transform.position;
		GetComponent<Rigidbody2D> ().velocity = -vel.normalized;
	}
}

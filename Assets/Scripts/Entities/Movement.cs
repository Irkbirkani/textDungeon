using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public bool moving = false;
	public Transform newPos;
	private float mag = 0.01f;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

		if (moving)
		{
			var offset = newPos.position - GetComponent<Transform>().position;
			if (Mathf.Abs(offset.magnitude) > mag)
			{
				moveTo(newPos);
			}
			else
			{
				moving = false;
				mag = 0.01f;
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			}
		}
	}

	public void moveTo(Transform newPos) {
		Vector2 vel = GetComponent<Transform> ().position - newPos.transform.position;
		GetComponent<Rigidbody2D> ().velocity = -vel.normalized;
	}
	public void SetMove(bool mv, Transform np, float m){
		mag = m;	
		moving = mv;
		newPos = np;
	}
}

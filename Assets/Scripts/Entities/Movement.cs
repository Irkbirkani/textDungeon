using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public bool moving = false;
	public Transform newPos;
    private float mag = 0.01f;
    private Object tgt;

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
                if (tgt != null)
                    GetComponent<Entity>().location.SortMoveObject(tgt);
                tgt = null;
			}
		}
	}

	public void moveTo(Transform newPos) {
		Vector2 vel = GetComponent<Transform> ().position - newPos.transform.position;
		GetComponent<Rigidbody2D> ().velocity = -vel.normalized;
	}
    public void moveToTarget(Transform np,float offset, Object obj)
    {
        moving = true;
        newPos = np;
        tgt = obj;
        mag = offset;
    }
	public void SetMove(bool mv, Transform np, float m){
		mag = m;	
		moving = mv;
		newPos = np;
	}
}

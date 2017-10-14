using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public bool moving = false;
    public Transform newPos;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (moving)
        {
            var offset = newPos.position - GetComponent<Transform>().position;
            if (offset.magnitude > 0.01f)
            {
                moveTo(newPos);
            }
            else
            {
                moving = false;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
    }

    public void moveTo(Transform newPos) {
        Vector2 vel = GetComponent<Transform>().position - newPos.transform.position;
        GetComponent<Rigidbody2D>().velocity = -vel.normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            //other.gameObject.GetComponent<Exit>().
        }
    }
}

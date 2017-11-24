using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public string Name = "You";
    private Room location;
    private int _Health = 100, _Mana = 100, _Stamina = 100, _Level = 1;
    private bool exiting = false;
    private bool isPlayer = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int Mana()
    {
        return _Mana;
    }

    public int Health()
    {
        return _Health;
    }

    public int Stamina()
    {
        return _Stamina;
    }

    public int Level()
    {
        return _Level;
    }
    public Room Location()
    {
        return location;
    }

    public void setIsPlayer(bool pl)
    {
        isPlayer = pl;
    }

    public bool IsPlayer()
    {
        return isPlayer;
    }
    public void SetPlayer()
    {
        isPlayer = true;
    }

    public void SetLocation(Room newLoc)
    {
        if (location != null)
            location.transform.position = new Vector2(0, 200);
        newLoc.transform.position = new Vector2(0, 2);
        location = newLoc;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            exiting = !exiting;
            if (exiting)
            {
                SetLocation(other.gameObject.GetComponent<Exit>().goesTo.GetParent().GetComponent<Room>());
                other.gameObject.GetComponent<Exit>().goesTo.GetParent().GetComponent<Room>().AddEntity(this);
                transform.position = other.gameObject.GetComponent<Exit>().goesTo.transform.position;
                GetComponent<Movement>().newPos = location.transform;
            }
        }
    }
}

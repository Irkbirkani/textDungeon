using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private string _Name = "";
	private Room location;
    private int _Health = 100, _Mana = 100, _Stamina = 100, _Level = 1; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string Name()
    {
        return _Name;
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
	public Room Location(){
		return location;
	}

	public void SetLocation(Room newLoc){
		location = newLoc;
	}
}

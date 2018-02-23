using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public string Name = "You";
    public Room location;
	private float health = 90.0f, mana = 99.0f, stamina = 99.0f, strength = 10.0f, intel = 10.0f;
    private float maxHealth = 90.0f, maxMana = 99.0f, maxStamina = 99.0f;
	private int level = 1;
    private bool exiting = false;
    public bool isPlayer = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		
    }

    public float Mana()
    {
		return mana+(intel/10.0f);
    }

    public float Health()
    {
		return health+(level*10);
    }

    public float Stamina()
    {
		return stamina+(strength/10.0f);
    }

    public float MaxMana()
    {
        return maxMana + (intel / 10.0f);
    }

    public float MaxHealth()
    {
        return maxHealth + (level * 10);
    }

    public float MaxStamina()
    {
        return maxStamina + (strength / 10.0f);
    }

    public int Level()
    {
        return level;
    }
    public Room Location()
    {
        return location;
    }

    public bool IsPlayer()
    {
        return isPlayer;
    }
   
    public void SetStamina(int dec)
    {
        if (stamina + dec >= MaxStamina())
            stamina = MaxStamina();
        else if (stamina + dec <= 0)
            stamina = 0;
        else
            stamina = stamina + dec;
    }
    public void SetHealth(int dec)
    {
        if (health + dec >= MaxHealth())
            health = MaxHealth();
        else if (health + dec <= 0)
            health = 0;
        else
            health = health + dec;
    }
    public void SetMana(int dec)
    {
        if (mana + dec >= MaxMana())
            mana = MaxMana();
        else if (mana + dec <= 0)
            mana = 0;
        else
            mana = stamina + dec;
    }

    public void SetLocation(Room newLoc)
    {
        if (location != null)
			location.transform.position = location.startLoc;
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
				location.RemoveEntity (this);
                SetLocation(other.gameObject.GetComponent<Exit>().goesTo.GetParent().GetComponent<Room>());
                location.AddEntity(this);
                transform.position = other.gameObject.GetComponent<Exit>().goesTo.transform.position;
                GetComponent<Movement>().newPos = location.transform;
            }
        }
    }
}

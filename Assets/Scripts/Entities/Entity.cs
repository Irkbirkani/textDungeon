using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class Entity : MonoBehaviour {

    public string Name = "You";
    public Room location;
    public float atkSpeed = 2.0f;
    public int regen = 2;
    public int restRegen = 5;
    public bool isPlayer = false;
    public bool attacking = false;
    public bool resting = false;
    public UpdatePlayerStats TargetPanel; 

	private float health = 90.0f, mana = 99.0f, stamina = 99.0f, strength = 10.0f, intel = 10.0f;
    private float maxHealth = 90.0f, maxMana = 99.0f, maxStamina = 99.0f;
	private int level = 1;
    private bool exiting = false;
    private Entity target;
    private bool dead = false;
    private List<Item> inventory = new List<Item>();
    private int maxInventorySize = 10;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            dead = true;
        if (dead)
        {
            gameObject.SetActive(false);
        }
        if (target != null && target.dead)
            TargetPanel.gameObject.SetActive(false);
    }

    public float Mana() { return mana+(intel/10.0f); }

    public float Health() { return health+(level*10); }

    public float Stamina() { return stamina+(strength/10.0f); }

    public float MaxMana() { return maxMana + (intel / 10.0f); }

    public float MaxHealth() { return maxHealth + (level * 10); }

    public float MaxStamina() { return maxStamina + (strength / 10.0f); }

    public int Level() { return level; }

    public float Strength() { return strength; }

    public float Intel() { return intel; }

    public Room Location() { return location; }

    public bool IsPlayer() { return isPlayer; }
    
    public Entity Target() { return target; }
    
    public void SetTarget(Entity t)
    {
        target = t;
        TargetPanel.player = target;
        TargetPanel.gameObject.SetActive(true);
    }


    // Inventory Management
    public List<Item> Inventory() { return inventory; }

    //Prints the inventory to the infopanel
    public string PrintInventory()
    {
        string ret = "";
        for (int i = 0; i < inventory.Count; ++i)
        {
            ret = ret + inventory[i].name + "\n";
        }
        return ret;
    }

    //Adds An item to the inventory, but only if the inventory isnt full.
    public bool AddToInventory(Item i)
    {
        if (inventory.Count >= maxInventorySize)
            return false;
        inventory.Add(i);
        return true;
    }

    //Checks if an Item is in the inventory
    public bool InInventory(string itm)
    {
        foreach (Item i in inventory)
        {
            if (i.name.ToLower().Equals(itm.ToLower()))
            {
                return true;
            }
        }
        return false;
    }

    //Get's an Item from the inventory
    public Item GetItem(string itm)
    {    
        foreach (Item i in inventory)
        {
            if (i.name.ToLower().Equals(itm.ToLower()))
            {
                
                return i;
            }
        }
        return null;
    }

    //Removes an item from the inventory and puts it in the current room.
    public void DropItem(string itm)
    {
        if (InInventory(itm))
        {
            Item i = GetItem(itm);
            inventory.Remove(i);
            location.AddItem(i);
        }
        else
        {
            location.print("< Item not in inventory.", "#00ffffff");
        }
    }
   
    /*
     * The below three functions, SetStamina, SetHealth, SetMana do the same thing with their respective stat.
     */

    public void SetStamina(float dec)
    {
        Debug.Log(name + " MaxStamina: " + MaxStamina() + " Amount Added: " + dec);
        if (Stamina() + dec > MaxStamina())
        {
            stamina = maxStamina;
            Debug.Log(name + " MaxStamina: " + maxStamina + " Stamina: " + stamina);
        }
        else if (Stamina() + dec <= 0)
        {
            Debug.Log(name + " MaxStamina: " + maxStamina + " Stamina: " + stamina);
            stamina = 0;
        }
        else
        {
            Debug.Log(name + " MaxStamina: " + maxStamina + " Stamina: " + stamina);
            stamina = stamina + dec;
        }
    }

    public void SetHealth(float dec)
    {
        if ((Health() + dec) >= MaxHealth())
            health = maxHealth;
        else if (Health() + dec <= 0)
            health = 0;
        else
            health = health + dec;
    }

    public void SetMana(float dec)
    {
        if (Mana() + dec > MaxMana())
            mana = maxMana;
        else if (Mana() + dec <= 0)
            mana = 0;
        else
            mana = stamina + dec;
    }

    /*
     * Below is combat math and management   
     */   
    void LeaveCombat()
    {
        if (isPlayer)
        {
            GameObject chat = GameObject.Find("PlayerControl").gameObject;
            chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(0, 0, 1);
            chat.GetComponent<UpdateChatText>().UpdateChat("<color=#ff0000ff>> Leaving Combat</color>");
            chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(1, 1, 1);
            chat.transform.Find("ScrollView").gameObject.GetComponent<ScrollRect>().gameObject.transform.Find("Scrollbar Vertical").gameObject.GetComponent<Scrollbar>().value = 0.0f;
            this.GetComponent<Movement>().SetMove(true,location.transform, 0.01f);
        }
        attacking = false;
        target = null;
    }

    public void TakeDamage(float damage, Entity e)
    {
        if (Health()-damage > 0)
        {
            if (isPlayer)
            {
                GameObject chat = GameObject.Find("PlayerControl").gameObject;
                chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(0, 0, 1);
                chat.GetComponent<UpdateChatText>().UpdateChat("<color=#ff0000ff>< Enemy hit you for "+ damage +" damage!</color>");
                chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(1, 1, 1);
                chat.transform.Find("ScrollView").gameObject.GetComponent<ScrollRect>().gameObject.transform.Find("Scrollbar Vertical").gameObject.GetComponent<Scrollbar>().value = 0.0f;
            }
            SetHealth(-damage);
            Attack(e);
        }
        else
        {
            LeaveCombat();
            e.LeaveCombat();
        }
    }

    public void DealDamage()
    {
        float damage = strength + Random.Range(-5, 5);
        if (this.isPlayer && target.Name != null)
        {
            GameObject chat = GameObject.Find("PlayerControl").gameObject;
            chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(0, 0, 1);
            chat.GetComponent<UpdateChatText>().UpdateChat("<color=#ff0000ff>>" + " Attacking " + target.name + " for " + damage + " damage! " + target.Name + " has " + (target.Health() - damage) + " health left!</color>");
            chat.transform.Find("ScrollView").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<Text>().color = new Color(1, 1, 1);
            chat.transform.Find("ScrollView").gameObject.GetComponent<ScrollRect>().gameObject.transform.Find("Scrollbar Vertical").gameObject.GetComponent<Scrollbar>().value = 0.0f;
        }
        target.TakeDamage(damage, this);
    }

    public void Attack(Entity e)
    {
        if (e.location == location)
        {
            target = e;
            attacking = true;
            Invoke("DealDamage", atkSpeed);
        } else
        {
            LeaveCombat();
            e.LeaveCombat();
        }
    }

    public void SetAttack(Entity e)
    {
        resting = false;
        GetComponent<Movement>().moveToTarget(e.transform, 1.0f, e);
        SetTarget(e);
        Attack(e);
    }

    public void SetLocation(Room newLoc)
    {
        if (location != null)
            location.gameObject.SetActive(false);

        newLoc.gameObject.SetActive(true);
        location = newLoc;
    }
    
    public void MakeEntity(XmlNode node, bool player)
    {
        if(!player)
            this.transform.localPosition = new Vector2(float.Parse(node.SelectSingleNode("xCoor").InnerText), float.Parse(node.SelectSingleNode("yCoor").InnerText));

        this.name = node.Attributes["name"].InnerText;
        this.GetComponent<Entity>().Name = node.Attributes["name"].InnerText;
        level        = int.Parse(node.SelectSingleNode("level").InnerText);
        intel        = float.Parse(node.SelectSingleNode("intellegence").InnerText);
        strength     = float.Parse(node.SelectSingleNode("strength").InnerText);
        atkSpeed     = float.Parse(node.SelectSingleNode("attackSpeed").InnerText);
        health       = float.Parse(node.SelectSingleNode("health").InnerText);
        maxHealth    = float.Parse(node.SelectSingleNode("health").InnerText);
        mana         = float.Parse(node.SelectSingleNode("mana").InnerText);
        maxMana      = float.Parse(node.SelectSingleNode("mana").InnerText);
        stamina      = float.Parse(node.SelectSingleNode("stamina").InnerText);
        maxStamina   = float.Parse(node.SelectSingleNode("stamina").InnerText);
        isPlayer     = player;
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

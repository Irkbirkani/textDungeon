using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRegen : MonoBehaviour {

    public int delay = 2;

    private Entity ent;
	 //Use this for initialization
	void Start () {
       ent = this.gameObject.GetComponent<Entity>();
       StartCoroutine(Regen());
	}

    void Update()
    {
    }


    IEnumerator Regen()
    { 
        if (!ent.attacking)
        {
            ent.SetStamina(ent.resting ? ent.restRegen : ent.regen);
            ent.SetHealth(ent.resting ? ent.restRegen : ent.regen);
            ent.SetMana(ent.resting ? ent.restRegen : ent.regen);
            if (ent.Stamina() - ent.MaxStamina() <= 0.001 && ent.Health() - ent.MaxHealth() <= 0.001 && ent.Mana() - ent.MaxMana() <= 0.001)
                ent.resting = false;
        }
        yield return new WaitForSeconds(delay);

        StartCoroutine(Regen());
    }
}

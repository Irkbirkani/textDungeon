using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRegen : MonoBehaviour {

    public int delay = 2;

    private Entity ent;
	// Use this for initialization
	void Start () {
        ent = this.gameObject.GetComponent<Entity>();
        StartCoroutine(Regen());
	}


    IEnumerator Regen()
    {
            if (!ent.attacking)
            {
                ent.SetStamina(ent.resting ? ent.restRegen : ent.regen);
                ent.SetHealth(ent.resting ? ent.restRegen : ent.regen);
                ent.SetMana(ent.resting ? ent.restRegen : ent.regen);
                if (ent.Stamina() == ent.MaxStamina() && ent.Health() == ent.MaxHealth() && ent.Mana() == ent.MaxMana())
                    ent.resting = false;
            }
            yield return new WaitForSeconds(delay);

        StartCoroutine(Regen());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

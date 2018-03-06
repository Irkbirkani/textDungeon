using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour {

    private Entity ent;
    private Entity target;

	// Use this for initialization
	void Start () {
        ent = this.gameObject.GetComponent<Entity>();
        StartCoroutine(Attack());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Attack()
    {
        //for(; ; )
        //{
            Debug.Log("Hello");
            if (ent.attacking)
            {
                yield return new WaitForSeconds(ent.atkSpeed);
                // Debug.Log("attacking");
                // float dmg = Random.Range(-5, 5) + ent.Strength();
                // if (target.Health()-dmg > 0)
                // {
                //     Debug.Log("dealing damage");
                //     target.SetHealth(-dmg);
                //     target.Attack(ent);
                // } else
                // {
                //     target.attacking = false;
                //     ent.attacking = false;
                // }

            }
        //}
    }

    public void SetTarget(Entity e)
    {
        target = e;
    }
}

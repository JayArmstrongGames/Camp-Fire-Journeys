using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeManager : MonoBehaviour {
	List<Collider2D> colliders = new List<Collider2D>();
	// Use this for initialization
	void Start () {
		GetComponent<BoxCollider2D>().enabled = false;
	}

	public void Attack()
	{
		GetComponent<BoxCollider2D>().enabled = true;
		Invoke("endAttack", 0.1f);
	}

	void endAttack()
	{
		GetComponent<BoxCollider2D>().enabled = false;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log(col);
		if (col.rigidbody2D != null)
		{
			Vector2 knockBack = col.rigidbody2D.velocity;
			knockBack.x += 10f * GetComponentInParent<Movement>().facing.x;
			col.rigidbody2D.velocity = knockBack;
		}  

		if (col.transform.parent != null)
		{
			UnitInfo unitinfo = col.transform.parent.gameObject.GetComponent<UnitInfo>();
			
			if (unitinfo != null)
			{
				if (unitinfo.Team != GetComponentInParent<UnitInfo>().Team)
				{
					unitinfo.Damage(0.5f);
				}
			} 
		} 

	}

}

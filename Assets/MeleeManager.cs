using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeManager : MonoBehaviour {

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
		/*
		if (col.rigidbody2D != null)
		{
			Vector2 knockBack = col.rigidbody2D.velocity;
			knockBack.x += 10f * GetComponentInParent<Movement>().facing.x;
			col.rigidbody2D.velocity = knockBack;
		} 
		*/

		if (col.gameObject.layer == LayerMask.NameToLayer("TakeDamage"))
		{
			Debug.Log ("HIT!");
			DamageBox damagebox = col.GetComponent<DamageBox>();
			Debug.Log(transform.parent.gameObject.GetComponentInChildren<DamageBox>().Team +    "      " + damagebox.Team);
			if (damagebox.Team != transform.parent.gameObject.GetComponentInChildren<DamageBox>().Team)
			{
				Debug.Log("NOT TEAM!");
				damagebox.Damage(0.5f);
			}
		} 

			if (col.rigidbody2D != null)
			{
			Debug.Log("explosion");
				Rigidbody2D r = col.rigidbody2D;
				if (Vector2.Distance(r.transform.position, transform.position) < 6) {
					float px = r.transform.position.x - transform.position.x;
					float py = r.transform.position.y - transform.position.y;
					r.AddForce(new Vector2(px, py).normalized * 500 /  Vector2.Distance(r.transform.position, transform.position));

				}
			}

	}

}

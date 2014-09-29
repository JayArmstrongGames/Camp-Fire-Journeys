using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeManager : MonoBehaviour {

	BoxCollider2D hitBox;
	// Use this for initialization
	void Start () {
		hitBox = GetComponent<BoxCollider2D>();
		hitBox.enabled = false;
	}

	public void Attack()
	{
		hitBox.enabled = true;
		Vector3 position = hitBox.transform.localPosition;
		position.x = hitBox.bounds.size.x / 2 * transform.parent.GetComponent<Movement>().facing.x;
		hitBox.transform.localPosition = position;
		Invoke("endAttack", 0.1f);
	}

	void endAttack()
	{
		Vector3 position = new Vector3(0,0);
		hitBox.transform.localPosition = position;
		hitBox.enabled = false;
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
		
		DamageBox damagebox = col.gameObject.GetComponent<DamageBox>();
		if (damagebox != null)
		{
			damagebox.Damage(0.5f);

			Vector2 velocity = col.gameObject.transform.parent.rigidbody2D.velocity;
			velocity.x = 10.0f * transform.parent.GetComponent<Movement>().facing.x;
			col.gameObject.transform.parent.rigidbody2D.velocity = velocity;

		
				
		} 





	}

}

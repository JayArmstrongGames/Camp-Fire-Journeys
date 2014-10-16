using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeManager : MonoBehaviour {

	BoxCollider2D hitBox;
	string thrustDirection;

	void Start () {
		hitBox = GetComponent<BoxCollider2D>();
		hitBox.enabled = false;
	}

	public void Attack(string thrustDirection = "")
	{
		this.thrustDirection = thrustDirection;
		hitBox.enabled = true;
	
		Vector3 position = hitBox.transform.localPosition;
		switch(thrustDirection)
		{
		case "CHARGEUP":
			position.y = hitBox.bounds.size.y / 2;
			break;
		default:
			position.x = hitBox.bounds.size.x / 2 * transform.parent.GetComponent<Movement>().facing.x;
			break;
		}
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

		if (col.gameObject.layer == LayerMask.NameToLayer("Bullets"))
		{
		
			Quaternion reflectAngle = col.transform.rotation;

			if (col.transform.rotation.z == 0)
			{
				reflectAngle.z = 180f;
			} else {
				reflectAngle.z = 0f;
			}
			col.transform.rotation = reflectAngle;
		
			Debug.Log("Deflect bullet");
			return;
		}

		if (col.rigidbody2D != null)
		{

			Debug.Log (col + "   " + thrustDirection);
			switch (thrustDirection)
			{
				case "UP":
					col.rigidbody2D.velocity = new Vector2(0f, 10f);
					Debug.Log(col.rigidbody2D.velocity);
				break;
				default:
		
					col.rigidbody2D.velocity = new Vector2(15f *transform.parent.GetComponent<Movement>().facing.x, 3f);
					//col.rigidbody2D.AddForce(Vector2.right * 400f * transform.parent.GetComponent<Movement>().facing.x);
				break;
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

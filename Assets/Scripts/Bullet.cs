﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float Speed;
	public int Team;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 velocity = rigidbody2D.velocity;
		velocity.x = Speed * transform.localScale.x;
		rigidbody2D.velocity = velocity;
	}

	void OnTriggerEnter2D(Collider2D col)
	{


		if (col.rigidbody2D != null)
		{
			col.rigidbody2D.AddForceAtPosition(rigidbody2D.velocity * 25, transform.position);
		}                              

		if (col.transform.parent != null)
		{
			UnitInfo unitinfo = col.transform.parent.gameObject.GetComponent<UnitInfo>();
		
			if (unitinfo != null)
			{
				if (unitinfo.Team != Team)
				{
					unitinfo.Damage(0.5f);
					Destroy(gameObject);
				}
			} else {
				Destroy(gameObject);
			}
			} else {
				Destroy(gameObject);
		}
	}
}

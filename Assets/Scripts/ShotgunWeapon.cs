using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShotgunWeapon : Weapon {

	public override void Fire()
	{
		if (Time.time > wait){
		wait = Time.time + RateOfFire;
	
		for (int i = 0; i < 3; i++)
		{
			Vector3 position = Vector3.up * i * 0.2f;
			Bullet newbullet = Instantiate(bullet, transform.position + position, transform.rotation) as Bullet;
			if (transform.localScale.x < 0)
			{
				Quaternion bulletRotation = newbullet.transform.rotation;
				bulletRotation.z = 180f;
				newbullet.transform.rotation = bulletRotation;
			}
			UnitStats unitinfo = gameObject.transform.parent.GetComponentInParent<UnitStats>();
			newbullet.Team = unitinfo.Team;
		}


		if (spriteparticlemanager == null)return;
		spriteparticlemanager.Add(transform.position, new Vector2(-3 * transform.localScale.x, 7), 3f);
		//Create light flash
		}
	}


	
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {
	

	public Bullet bullet;
	public float RateOfFire;

	[HideInInspector]
	public SpriteParticleManager spriteparticlemanager;
	public float wait =0.0f;


	void Start()
	{
		spriteparticlemanager = GameObject.FindGameObjectWithTag("SpriteParticle Manager").GetComponent<SpriteParticleManager>();
		transform.localScale = transform.parent.parent.GetComponentInChildren<SpriteRenderer>().transform.localScale;
	}

	public virtual void Fire()
	{
		if (Time.time > wait){
		wait = Time.time + RateOfFire;
		Bullet newbullet = Instantiate(bullet, transform.position, transform.rotation) as Bullet;
		newbullet.transform.localScale = transform.localScale;
		DamageBox unitinfo = transform.parent.parent.GetComponent<DamageBox>();
		newbullet.Team = unitinfo.Team;
		if (spriteparticlemanager == null)return;
		spriteparticlemanager.Add(transform.position, new Vector2(-3 * transform.localScale.x, 7), 3f);
		//Create light flash
		}
	}


	
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour {

	public Weapon CurrentWeapon;
	public AllWeapons allweapons;


	// Use this for initialization
	void Start () {
		ChangeGun(CurrentWeapon);
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void Fire()
	{
		CurrentWeapon.transform.localScale = transform.localScale;
		CurrentWeapon.Fire();
	}

	public void ChangeGun(Weapon weapon)
	{
		DestroyGun();
		CurrentWeapon = Instantiate(weapon, transform.position, transform.rotation) as Weapon;
		CurrentWeapon.transform.parent = transform;
	}

	void DestroyGun()
	{
		if (GameObject.Find(CurrentWeapon.name))
		{
			Destroy(CurrentWeapon.gameObject);
		}
	}
}

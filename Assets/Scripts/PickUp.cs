using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	WeaponManager weaponmanager;
	bool disable;
	public AllWeapons allweapons;
	public HUDText hudText;


	void OnTriggerEnter2D(Collider2D collider)
	{
		if (disable)return;
		if (collider.transform.parent == null)return;

		weaponmanager = collider.transform.parent.GetComponentInChildren<WeaponManager>();

		if (weaponmanager != null)
		{
			disable = true;
			Invoke("Remove", 0.1f);
		}
	}

	void Remove ()
	{
		int randomWeapon = Random.Range(0, allweapons.WeaponPrefabs.Count);
		hudText.Add(allweapons.WeaponNames[randomWeapon] + "!", Color.white, 0f);
		weaponmanager.SendMessage("ChangeGun", allweapons.WeaponPrefabs[randomWeapon]);
		Invoke("Restock", 5.0f);
		gameObject.SetActive(false);
	}
	void Restock ()
	{
		GameScript gamescript = GameObject.FindGameObjectWithTag("Game").GetComponent<GameScript>();
		transform.position = gamescript.GetNextSpawnPoint();
		disable = false;
		gameObject.SetActive(true);
	}
}



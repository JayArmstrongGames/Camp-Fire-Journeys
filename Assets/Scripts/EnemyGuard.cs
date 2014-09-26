using UnityEngine;
using System.Collections;

public class EnemyGuard : MonoBehaviour {

	PlayerDevice device;
	Movement movement;
	DamageBox unitinfo;
	public WeaponManager weaponmanager;
	public int LineOfSight;


	// Use this for initialization
	void Start () {
		unitinfo = gameObject.GetComponent<DamageBox>();
		movement = gameObject.GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.right * movement.direction, 5);

		for (int i = 0; i < hit.Length; i++)
		{
			if (hit[i].collider.transform.parent == null)return;
			DamageBox colliderunitinfo = hit[i].collider.transform.parent.gameObject.GetComponent<DamageBox>();
			if (colliderunitinfo != null)
			{
				if (colliderunitinfo.Team != unitinfo.Team)
				{
					weaponmanager.Fire();
				}
			}
			Debug.DrawLine(transform.position, hit[i].point, Color.yellow);
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IsBoppable : MonoBehaviour {

	DamageBox unitinfo;
	public int Team;
	public LayerMask ignoreLayerMask;

	void Start () {
		unitinfo = transform.parent.GetComponent<DamageBox>();
		if (unitinfo != null)
		{
			Team = unitinfo.Team;
		} else {
			Team = -1;
		}
	}

	void OnBop()
	{
		if (unitinfo == null)return;
		unitinfo.Damage(2f);
	}

}

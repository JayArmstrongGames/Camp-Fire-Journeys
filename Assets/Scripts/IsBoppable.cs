using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IsBoppable : MonoBehaviour {

	UnitInfo unitinfo;
	public LayerMask ignoreLayerMask;

	void Start () {
		unitinfo = transform.parent.GetComponent<UnitInfo>();
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if ( ( ( 1 << collider.gameObject.layer ) & ignoreLayerMask ) > 0 ) return;

		UnitInfo colliderunitInfo = collider.transform.parent.gameObject.GetComponent<UnitInfo>();

		if (colliderunitInfo != null)
		{
			if (collider.name == "Floor collider" && collider.transform.parent.gameObject.rigidbody2D.velocity.y < 0){

				Vector2 bounce = new Vector2(0,10);
				collider.transform.parent.gameObject.rigidbody2D.velocity = bounce;
				if (colliderunitInfo.Team != unitinfo.Team)
				{
					unitinfo.Damage(2f);
				}
			}
		}
	}
}

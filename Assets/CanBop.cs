using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Collider2D))]

public class CanBop : MonoBehaviour {

	public int Team = -1;

	// Use this for initialization
	void Start () {
		DamageBox info = gameObject.GetComponent<DamageBox>();
		if (info != null)
		{
			Team = info.Team;
		} 
		Debug.Log("unfo " +  info);
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		IsBoppable isboppable = collider.gameObject.GetComponent<IsBoppable>();
		if (isboppable == null)return;

		Bop (isboppable);
	}

	void OnCollisionEnter2D(Collision2D collider)
	{
		IsBoppable isboppable = collider.gameObject.GetComponent<IsBoppable>();
		if (isboppable == null)return;
		
		Bop (isboppable);

	}

	void Bop(IsBoppable isboppable)
	{
		if (gameObject.rigidbody2D == null)return;
		if (gameObject.rigidbody2D.velocity.y < 0 && isboppable.Team != Team)
		{
			Vector2 bounce = new Vector2(0,10);
			gameObject.rigidbody2D.velocity = bounce;
			isboppable.SendMessage("OnBop");
		}
	}
}

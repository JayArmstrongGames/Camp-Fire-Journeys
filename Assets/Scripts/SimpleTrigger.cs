using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SimpleTrigger : MonoBehaviour 
{
	List<Collider2D> colliders = new List<Collider2D>();

	public LayerMask ignoreLayerMask;

	public List<Collider2D> ignoreColliders;

	public event Action<SimpleTrigger, Collider2D> OnEnter;
	public event Action<SimpleTrigger, Collider2D> OnExit;
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if ( ( ( 1 << collider.gameObject.layer ) & ignoreLayerMask ) > 0 ) return;
		if ( ignoreColliders.Contains( collider ) ) return;
		if (colliders.Contains (collider )) return;
		colliders.Add( collider );
		if ( OnEnter != null )
			OnEnter( this, collider );
	}

	void OnTriggerExit2D( Collider2D collider )
	{
		if ( ( ( 1 << collider.gameObject.layer ) & ignoreLayerMask ) > 0 ) return;
		if ( ignoreColliders.Contains( collider ) ) return;
		colliders.Remove( collider );
		if ( OnExit != null )
			OnExit( this, collider );
	}

	void OnDisable()
	{
		colliders.RemoveRange( 0, colliders.Count );
	}

	public bool IsColliding
	{
		get
		{
			return colliders.Count > 0;
		}
	}

	public int TotalCollidersTouching
	{
		get
		{
			return colliders.Count;
		}
	}

	void FixedUpdate()
	{
		Collider2D col;

		for ( int i = 0; i < colliders.Count; i++ )
		{
			col = colliders[i];
			if ( col == null || !col.gameObject.activeSelf )
			{
				colliders.RemoveAt( i );
				i--;
			}
		}
	}
}

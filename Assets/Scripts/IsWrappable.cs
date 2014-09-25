using UnityEngine;
using System.Collections;

public class IsWrappable : MonoBehaviour {

	LevelInfo level;

	void Start()
	{
		GameObject gameGameObject = GameObject.FindGameObjectWithTag( "Level" );
		level = gameGameObject.GetComponent<LevelInfo>(); 
	}

	void Update () {

		Vector3 position = transform.position;

		if (transform.position.y < -1)
		{
			position.y = 20.0f - 1;
			transform.position = position;
			if (rigidbody2D.velocity.y < -15)
			{
				position = new Vector2(rigidbody2D.velocity.x, -15);
				rigidbody2D.velocity = position;
			}
			return;
		}
		if (transform.position.y > 20.0f - 1)
		{
			position.y = -1;
			transform.position = position;
			return;
		}
		if (transform.position.x < 0 )
		{
			position.x = 35.0f - 0.5f;
			transform.position = position;
			return;
		}
		if (transform.position.x >  35.0f - 0.5f)
		{
			position.x =  0;
			transform.position = position;
			return;
		}
	}



}

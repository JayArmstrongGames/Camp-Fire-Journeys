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
			position.y = level.tileMap.height - 1;
			transform.position = position;
			if (rigidbody2D.velocity.y < -15)
			{
				position = new Vector2(rigidbody2D.velocity.x, -15);
				rigidbody2D.velocity = position;
			}
			return;
		}
		if (transform.position.y > level.tileMap.height - 1)
		{
			position.y = -1;
			transform.position = position;
			return;
		}
		if (transform.position.x < 0.5 -level.tileMap.width/2)
		{
			position.x = 1.5f + level.tileMap.width/2;
			transform.position = position;
			return;
		}
		if (transform.position.x > 1.5 + level.tileMap.width/2)
		{
			position.x =  0.5f -level.tileMap.width/2;
			transform.position = position;
			return;
		}
	}



}

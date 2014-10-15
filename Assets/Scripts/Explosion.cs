using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	
	public float explosion_delay = 1f;
	public float explosion_rate = 1f;
	public float explosion_max_size = 10f;
	public float explosions_speed = 1f;
	public float current_radius = 0f;
	bool exploded = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		explosion_delay -= Time.deltaTime;
		if (explosion_delay < 0)
		{
			exploded = true;
		}

	}

	void FixedUpdate()
	{
		if (exploded == true)
		{
			if (current_radius < explosion_max_size)
			{
				current_radius += explosion_rate;
				CircleCollider2D explosion_radius = gameObject.GetComponent<CircleCollider2D>();
				explosion_radius.radius = current_radius;
			} else {
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (exploded == true)
		{
			if (col.gameObject.rigidbody2D != null)
			{
				Vector2 target = col.transform.position;
				Vector2 bomb = gameObject.transform.position;

				Vector2 direction = 20f * (bomb - target);

				col.gameObject.rigidbody2D.AddForce(direction);

			}

		}


	}

}

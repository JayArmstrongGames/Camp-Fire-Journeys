using UnityEngine;
using System.Collections;

public class fade : MonoBehaviour {

	public float modifier = 0.001f;
	public float starting = 0;

	SpriteRenderer sprite;


	void Start()
	{
		sprite = GetComponent<SpriteRenderer>();
		Color color = sprite.color;
		color.a += starting;
		sprite.color = color;
	}
	void Update () {

		if ((modifier > 0 && sprite.color.a < 1) || (modifier < 0 && sprite.color.a > 0))
		{
			Color color = sprite.color;
			color.a += modifier;
			sprite.color = color;
		}
	}
}

using UnityEngine;
using System.Collections;

public class fade : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if (sprite.color.a > 0)
		{
			Color color = sprite.color;
			color.a -= 0.001f;
			sprite.color = color;
		}
	}
}

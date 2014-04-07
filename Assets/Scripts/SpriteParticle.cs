using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteParticle : MonoBehaviour {
	
	SpriteRenderer sprite;
	public List<Sprite> SpriteList;
	public SpriteParticleManager manager;
	[HideInInspector]
	public float TimeOnScreen;

	void Start () {

	}

	public void Change()
	{
		sprite = gameObject.GetComponent<SpriteRenderer>();
		gameObject.SetActive(true);
		sprite.sprite = SpriteList[0];
		BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
		collider.size = sprite.bounds.size;
		Invoke("Remove", TimeOnScreen);
	}
	
	void Remove()
	{
		manager.Remove(this); 
		gameObject.SetActive(false);
	}
}

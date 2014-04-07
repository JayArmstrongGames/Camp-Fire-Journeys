using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteParticleManager : MonoBehaviour {

	List<SpriteParticle> ActiveSpriteParticles;
	List<SpriteParticle> InactiveSpriteParticles;
	public SpriteParticle spriteparticle;
	public float TimeOnScreen;

	// Use this for initialization
	void Start () {
		ActiveSpriteParticles = new List<SpriteParticle>();
		InactiveSpriteParticles = new List<SpriteParticle>();
	}

	public void Add(Vector3 position, Vector3 velocity, float torque)
	{
		SpriteParticle newspriteparticle;
		if (InactiveSpriteParticles.Count < 1)
		{
			newspriteparticle = Instantiate(spriteparticle, position, transform.rotation) as SpriteParticle;
			newspriteparticle.TimeOnScreen = TimeOnScreen;
		} else {
			newspriteparticle = InactiveSpriteParticles[0];
			newspriteparticle.transform.position = position;
			InactiveSpriteParticles.RemoveAt(0);
		}
			
			newspriteparticle.manager = this;
			ActiveSpriteParticles.Add(newspriteparticle);
			newspriteparticle.Change();
			newspriteparticle.rigidbody2D.velocity = velocity;
			newspriteparticle.rigidbody2D.AddTorque(torque);
	}

	public void Remove(SpriteParticle spriteparticletoremove)
	{
		ActiveSpriteParticles.Remove(spriteparticletoremove);
		InactiveSpriteParticles.Add (spriteparticletoremove);
	}
}

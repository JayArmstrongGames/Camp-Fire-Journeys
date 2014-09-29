using UnityEngine;
using System.Collections;

public class DamageBox : MonoBehaviour {
	
	public int Team = -999;
	public float TotalLife;
	UnitStats unitstats;
	float Life;
	
	
	void Start()
	{
		unitstats = GetComponentInParent<UnitStats>();
		if (Team == -999)Team = unitstats.Team;
		Reset();
	}
	
	void Reset()
	{
		ResetColor();
		Life = TotalLife;
	}
	
	public void Damage(float damage)
	{
		Debug.Log("BEING DAMAGED");
		Life -= damage;
		
		CancelInvoke("ResetColor");
		SpriteRenderer[] spriterenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < spriterenderers.Length; i++)
		{
			spriterenderers[i].material.color = Color.red;
		}

		SkeletonAnimation[] skeletons = gameObject.transform.parent.GetComponentsInChildren<SkeletonAnimation>();
		Debug.Log(skeletons.Length);
		for (int i = 0; i < skeletons.Length; i++)
		{
			skeletons[i].skeleton.r = 1;
			skeletons[i].skeleton.g = 0;
			skeletons[i].skeleton.b = 0;
		}


		Invoke("ResetColor", 0.2f);

		Debug.Log("life  " + Life);
		if (Life < 1f)
		{
			Invoke("kill", 0.1f);
			return;
		}
		
	}
	
	void ResetColor()
	{
		SpriteRenderer[] spriterenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < spriterenderers.Length; i++)
		{
			spriterenderers[i].material.color = Color.white;
		}

		SkeletonAnimation[] skeletons = gameObject.transform.parent.GetComponentsInChildren<SkeletonAnimation>();
		for (int i = 0; i < skeletons.Length; i++)
		{
			skeletons[i].skeleton.r = 1;
			skeletons[i].skeleton.g = 1;
			skeletons[i].skeleton.b = 1;
		}

	}
	
	void kill()
	{
		ScreenShake screenshake = Camera.main.GetComponent<ScreenShake>();
		screenshake.Shake(0.3f, 2f);
		Invoke("respawn", 2f);
		transform.parent.gameObject.SetActive(false);
	}
	
	void respawn()
	{
		transform.parent.gameObject.SetActive(true);
		Reset();
		GameScript gamescript = GameObject.FindGameObjectWithTag("Game").GetComponent<GameScript>();
		transform.parent.gameObject.transform.position = gamescript.GetNextSpawnPoint();
	}
	
}

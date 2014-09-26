using UnityEngine;
using System.Collections;

public class UnitStats : MonoBehaviour {

	public int Team;
	public float MoveSpeed;
	public float JumpHeight;
	public float TotalLife;
	float Life;


	void Start()
	{
		Reset();
	}

	void Reset()
	{
		ResetColor();
		Life = TotalLife;
	}

	public void Damage(float damage)
	{

		Life -= damage;

		CancelInvoke("ResetColor");
		SpriteRenderer[] spriterenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < spriterenderers.Length; i++)
		{
			spriterenderers[i].material.color = Color.red;
		}

		Invoke("ResetColor", 0.2f);

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
	}

	void kill()
	{
		ScreenShake screenshake = Camera.main.GetComponent<ScreenShake>();
		screenshake.Shake(0.3f, 2f);
		Invoke("respawn", 2f);
		gameObject.SetActive(false);
	}

	void respawn()
	{
		gameObject.SetActive(true);
		Reset();
		GameScript gamescript = GameObject.FindGameObjectWithTag("Game").GetComponent<GameScript>();
		transform.position = gamescript.GetNextSpawnPoint();
	}

}

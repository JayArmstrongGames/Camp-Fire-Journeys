using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour 
{
	public float ease = 10;
	float magnitude;
	float decay;

	bool isShaking;

	Vector3 OriginalPosition = new Vector3();

	public void Shake( float duration, float magnitude )
	{
	//	this.magnitude = magnitude;
	//	decay = magnitude / duration;
	}

	/*
	public bool IsShaking
	{
		get { return isShaking; }
	}

	void Update()
	{

		if ( magnitude > 0 )
		{
			if (isShaking != true){OriginalPosition = transform.position;};
			Vector3 target = transform.position + Random.insideUnitSphere * magnitude;
			transform.position = transform.position - ( ( ease * Time.deltaTime ) * ( transform.position - target ) * 0.5f ); ;
			magnitude -= decay * Time.deltaTime;
			isShaking = true;
		}
		else
		{
			if (transform.position != OriginalPosition && OriginalPosition != new Vector3())
			{
				Vector3 position = transform.position;
				position.x = Mathf.Lerp(position.x, OriginalPosition.x, Time.time);
				position.y = Mathf.Lerp(position.y, OriginalPosition.y, Time.time);
				transform.position = position;
			}
			//return Mathf.Lerp( minForce, maxForce, throwPower );
			isShaking = false;
		}

	}
	*/
}

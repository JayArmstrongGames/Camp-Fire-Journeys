using UnityEngine;
using System.Collections;

public class MaintainDistance : MonoBehaviour {

	public Transform host;
	public float offset = 1.2f;
	Vector3 maintaindistance;
	// Use this for initialization
	void Start () {
		maintaindistance = host.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = (maintaindistance + host.position) * offset;
		transform.position = position;

	}
}

using UnityEngine;
using System.Collections;

public class Enemy1 : MonoBehaviour {

	// Use this for initialization
	int dir;
	Movement movement;
	UnitInfo unitinfo;

	void Start () {
		dir = -1;
		unitinfo = gameObject.GetComponent<UnitInfo>();
		movement = gameObject.GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {
		movement.Move(unitinfo.MoveSpeed * dir);

		if (movement.lWall.IsColliding)
		{
			dir = 1;
			return;
		}
		if (movement.rWall.IsColliding)
		{
			dir = -1;
			return;
		}
	}
}

﻿using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Application.LoadLevelAdditive( "Level1" );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
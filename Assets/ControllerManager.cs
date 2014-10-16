using UnityEngine;
using System.Collections;

public class ControllerManager : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Activate(PlayerDevice device)
	{
		gameObject.SetActive(true);
		Debug.Log("new device: " + device);

		Main main = Main.GetInstance();
		int i = 0;
		Debug.Log(main.playerDevices.Count);



	}
}

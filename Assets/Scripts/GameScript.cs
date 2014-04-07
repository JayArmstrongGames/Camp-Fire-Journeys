using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	protected Main main;

	public GameObject playerPrefab;
	protected int lastSpawnPoint = -1;

	// Use this for initialization
	void Start () {
		main = Main.GetInstance();
		InitPlayers();




	}

	void InitPlayers()
	{
		for ( int i = 0; i < main.playerDevices.Count; i++ )
		{
			GameObject player = Instantiate( playerPrefab, transform.position + Vector3.up * 3, transform.rotation ) as GameObject;
			UnitInfo unitinfo = player.GetComponentInChildren<UnitInfo>();
			Debug.Log(unitinfo);
			unitinfo.Team = i + 5;
			player.transform.parent = transform;
			PlayerControl playercontrol = player.GetComponent<PlayerControl>();
			playercontrol.SetDevice( main.playerDevices[i] );
			player.SetActive( true );
		}

	}

	// Update is called once per frame
	void Update () {
		main.Update();
	}

	public Vector3 GetNextSpawnPoint()
	{

		GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag( "SpawnPoint" );
		if ( spawnPoints.Length == 0 ) return Vector3.zero;
		if ( lastSpawnPoint < 0 ) 
		{
			lastSpawnPoint = Random.Range( 0, spawnPoints.Length );
		}
		else
		{
			lastSpawnPoint++;
			if( lastSpawnPoint >= spawnPoints.Length )
				lastSpawnPoint = 0;
		}
		return spawnPoints[lastSpawnPoint].transform.position;
	}

	

}

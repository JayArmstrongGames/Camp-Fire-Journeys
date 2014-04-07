using UnityEngine;
using System.Collections;

public class LevelInfo : MonoBehaviour {

	public tk2dSpriteCollectionData thingYouWantToChangeItTo;
	public tk2dTileMap tileMap;

	void Start () {

		tileMap = GetComponentInChildren<tk2dTileMap>();
		tileMap.SpriteCollectionInst = thingYouWantToChangeItTo;
			
		tileMap.Build();

		Vector3 position = tileMap.transform.position;
		position.x += tileMap.width / 2;
		position.y += (tileMap.height / 2) - 0.5f;
		position.z = Camera.main.transform.position.z;
		Camera.main.transform.position = position;

		//mapdata.SetTile(0,1,0,2);
		//mapdata.SetTile(1,1,0,2);
		//mapdata.SetTile(2,1,0,1);
		//
	}

}

using UnityEngine;
using System.Collections;

public class LevelInfo : MonoBehaviour {

	public tk2dSpriteCollectionData thingYouWantToChangeItTo;
	public tk2dTileMap tileMap;

	private int currentColor = 0;
	public float changeTime;
	public Color[] colors;

	public SpriteRenderer[] fadein;
	public SpriteRenderer[] fadeout;

	private Light[] lights;
	public GameObject lightParent;

	void Start () {

		lights = lightParent.GetComponentsInChildren<Light>(true);

	//	tileMap = GetComponentInChildren<tk2dTileMap>();
	//	tileMap.SpriteCollectionInst = thingYouWantToChangeItTo;
			
	//	tileMap.Build();

		/*
		Vector3 position = tileMap.transform.position;
		position.x += tileMap.width / 2;
		position.y += (tileMap.height / 2) - 0.5f;
		position.z = Camera.main.transform.position.z;
		*/
		//Camera.main.transform.position = position;

		//mapdata.SetTile(0,1,0,2);
		//mapdata.SetTile(1,1,0,2);
		//mapdata.SetTile(2,1,0,1);
		//
	}

	
	
	void Update () {
		RenderSettings.ambientLight = Color.Lerp (RenderSettings.ambientLight, colors[currentColor], changeTime*Time.deltaTime);

		//float alpha = Mathf.Lerp(0.0f, (float) currentColor / colors.Length, changeTime*Time.deltaTime);

		for (int i = 0; i < fadein.Length; i++)
		{
			Color tempColor = fadein[i].color;
			if ((float) currentColor / (colors.Length - 1f) < 0.8f)
			{
				tempColor = Color.Lerp(fadein[i].color, colors[currentColor], changeTime*Time.deltaTime);
			}
			tempColor.a = (float) currentColor / (colors.Length - 1f);

			fadein[i].color = tempColor;
		}
		for (int i = 0; i < fadeout.Length; i++)
		{
			Color tempColor = Color.Lerp(fadeout[i].color, colors[currentColor], changeTime*Time.deltaTime);
			tempColor.a = 1f - ((float) currentColor / (colors.Length - 1f));
			fadeout[i].color = tempColor;
		}

		foreach (Light light in lights)
		{
			light.intensity =  ((float) currentColor / (colors.Length - 1f)) * 2.0f;
		}

		//this is just to test
		if(Input.GetKeyDown("space")){
			NextColor();
		}


		
	}
	
	void NextColor(){
		if(currentColor>=colors.Length-1){
			currentColor = 0;
		}else{
			currentColor +=1;
		}
	}
	
	void SetChangeTime(float ct){
		changeTime = ct;
	}
}

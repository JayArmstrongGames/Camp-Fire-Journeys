using UnityEngine;
using System.Collections;
using InControl;

public class PlayerControl : MonoBehaviour {

	PlayerDevice device;
	Movement movement;
	UnitInfo unitinfo;
	bool isMoving = false;
	//[HideInInspector]
	public WeaponManager weaponmanager;



	public void SetDevice( PlayerDevice device )
	{
		this.device = device;
		unitinfo = gameObject.GetComponent<UnitInfo>();
		movement = gameObject.GetComponent<Movement>();
	}

	void Update ()
	{
		UpdateInput();
	}


	void UpdateInput()
	{
		if (device == null)return;

		//RUN
		Vector2 inputMoveVector = device.GetInputMoveVector();
		movement.Move(unitinfo.MoveSpeed * inputMoveVector.x);
		if (inputMoveVector.x != 0.0f)
		{
			if (!isMoving)
			{
				SkeletonAnimation[] skeletons = gameObject.GetComponentsInChildren<SkeletonAnimation>();
				for (int i = 0; i < skeletons.Length; i++)
				{


					skeletons[i].transform.localScale = movement.facing;
					skeletons[i].state.Data.SetMix("Idle", "Run", 0.3f);
					skeletons[i].state.SetAnimation(0, "Run", true);


				}
			}
			isMoving = true;

		} else {
			if (isMoving == true)
			{
				SkeletonAnimation[] skeletons = gameObject.GetComponentsInChildren<SkeletonAnimation>();
				for (int i = 0; i < skeletons.Length; i++)
				{
					Debug.Log("animation " + skeletons[i].state.Data.SkeletonData.Animations[0].name);
					skeletons[i].transform.localScale = movement.facing;
					skeletons[i].state.Data.SetMix("Run", "Idle", 0.2f);
					skeletons[i].state.SetAnimation(0, "Idle", true);
				}
			}
			isMoving = false;
			
		}
	
		//JUMP
		if (device.GetAction1DownOnce())
		{
			movement.Jump(unitinfo.JumpHeight);
		}

		//FIRE
		if (device.GetAction2Down())
		{
			SkeletonAnimation[] skeletons = gameObject.GetComponentsInChildren<SkeletonAnimation>();
			for (int i = 0; i < skeletons.Length; i++)
			{
				skeletons[i].state.SetAnimation(0, "shoot", false);
			}
			weaponmanager.Fire();
		}

		//ATTACK
		if (device.GetAction3Down())
		{
			SkeletonAnimation[] skeletons = gameObject.GetComponentsInChildren<SkeletonAnimation>();
			for (int i = 0; i < skeletons.Length; i++)
			{
				skeletons[i].state.SetAnimation(0, "attack1", false);
			}
			weaponmanager.Fire();
		}



		//if ( inputMoveVector.x != 0.0f)
		//{
			//moveAbility.moveDirection.x = inputMoveVector.x;
			//moveAbility.moveDirection = game.trueNorth.TransformPoint( moveAbility.moveDirection );
			//Normalize movement vector to get full speed at all times
			//moveAbility.moveDirection.Normalize();
			//isMoving = true;
		//}
	}

	void render (float delta) {
		Debug.Log("BO");
	}
}

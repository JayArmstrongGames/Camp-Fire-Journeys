using UnityEngine;
using System.Collections;
using InControl;
using Spine;

public class PlayerControl : MonoBehaviour {

	PlayerDevice device;
	Movement movement;
	UnitInfo unitinfo;
	bool isMoving = false;
	//[HideInInspector]
	public WeaponManager weaponmanager;

		Bone head;

	public void SetDevice( PlayerDevice device )
	{
		this.device = device;
		unitinfo = gameObject.GetComponent<UnitInfo>();
		movement = gameObject.GetComponent<Movement>();

	}

	void Update ()
	{
		head = gameObject.GetComponentInChildren<SkeletonAnimation>().skeleton.FindBone("oldman_head");
		head.rotation += 0.5f;
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
					//skeletons[i].state.Data.SetMix("Idle", "Run", 0.3f);
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
				//	Debug.Log("animation " + skeletons[i].state.Data.SkeletonData.Animations[0].name);
					skeletons[i].transform.localScale = movement.facing;
					//skeletons[i].state.Data.SetMix("Run", "Idle", 0.2f);
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
		if (device.GetAction2UpOnce())
		{
			SkeletonAnimation[] skeletons = gameObject.GetComponentsInChildren<SkeletonAnimation>();
			for (int i = 0; i < skeletons.Length; i++)
			{
				skeletons[i].state.SetAnimation(1, "shoot", false);
			}
			weaponmanager.Fire();
		}

		//ATTACK
		if (device.GetAction3UpOnce())
		{
			SkeletonAnimation[] skeletons = gameObject.GetComponentsInChildren<SkeletonAnimation>();
			for (int i = 0; i < skeletons.Length; i++)
			{
				skeletons[i].state.SetAnimation(0, "attack1", false);
			}
		}




	}

	void render (float delta) {
		Debug.Log("BO");
	}
}

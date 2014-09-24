using UnityEngine;
using System.Collections;
using InControl;
using Spine;

public class PlayerControl : MonoBehaviour {

	PlayerDevice device;
	Movement movement;
	UnitInfo unitinfo;
	bool isMoving = false;
	int combo = 0;
	//[HideInInspector]
	public WeaponManager weaponmanager;

	Bone head;
	SkeletonAnimation skeletonAnimation;

	public void SetDevice( PlayerDevice device )
	{
		this.device = device;
		unitinfo = gameObject.GetComponent<UnitInfo>();
		movement = gameObject.GetComponent<Movement>();
		skeletonAnimation = gameObject.GetComponentInChildren<SkeletonAnimation>();
		skeletonAnimation.state.Event += Event;
		skeletonAnimation.UpdateBones += UpdateBones;
		head = gameObject.GetComponentInChildren<SkeletonAnimation>().skeleton.FindBone("head");
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
				skeletonAnimation.transform.localScale = movement.facing;
					//skeletons[i].state.Data.SetMix("Idle", "Run", 0.3f);
				skeletonAnimation.state.SetAnimation(0, "Run", true);
			}
			isMoving = true;

		} else {
			if (isMoving == true)
			{
				//	Debug.Log("animation " + skeletons[i].state.Data.SkeletonData.Animations[0].name);
				skeletonAnimation.transform.localScale = movement.facing;
					//skeletons[i].state.Data.SetMix("Run", "Idle", 0.2f);
				skeletonAnimation.state.SetAnimation(0, "Idle", true);
			}
			isMoving = false;
			
		}
	
		//JUMP
		if (device.GetAction1DownOnce())
		{
			movement.Jump(unitinfo.JumpHeight);
			skeletonAnimation.state.SetAnimation(0, "JumpMOCKUP", false);
		}

		//FIRE
		if (device.GetAction2DownOnce())
		{
			if (Time.time > weaponmanager.CurrentWeapon.wait)
			{
				skeletonAnimation.state.SetAnimation(1, "shoot", false);
			}
		}

		//ATTACK
		if (device.GetAction3DownOnce())
		{
			combo++; if (combo > 3)combo = 1;
			skeletonAnimation.state.SetAnimation(0, "attack" + combo, false);
		}

	}

	void Event(Spine.AnimationState state, int trackIndex, Spine.Event e)
	{
		switch (e.ToString())
		{
			case "ATTACK_HIT":
				Debug.Log ("SWING!");
			break;
			case "FIRE_GUN":
			Debug.Log(head.WorldX + "    " + head.WorldY);
			Bone gun = gameObject.GetComponentInChildren<SkeletonAnimation>().skeleton.FindBone("gun");

			weaponmanager.transform.position = new Vector3(transform.position.x + (gun.worldX * skeletonAnimation.transform.localScale.x), transform.position.y + gun.WorldY);
			weaponmanager.Fire();
			break;
		}
	}



	void UpdateBones(SkeletonAnimation skeletonAnimation) {
		//HEAD FOLLOW MOUSE
		// could be public
		const float LowerRotationBound = -60.0f;   
		const float UpperRotationBound = 60.0f;
		
		// temp variables
		float tempRot;
		Vector3 tempVec;

		// head bone rotation
		tempVec = Camera.main.WorldToScreenPoint(new Vector3(head.WorldX+transform.position.x, head.WorldY+transform.position.y, 0));
		tempVec = Input.mousePosition - tempVec;
		tempRot = Mathf.Atan2(tempVec.y, tempVec.x* skeletonAnimation.transform.localScale.x) * Mathf.Rad2Deg;
		head.Rotation = Mathf.Clamp(tempRot, LowerRotationBound, UpperRotationBound);


	}
}

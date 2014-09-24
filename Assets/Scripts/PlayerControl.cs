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
		skeletonAnimation.state.Event += ATTACK_HIT;
	}

	void Update ()
	{
		head = gameObject.GetComponentInChildren<SkeletonAnimation>().skeleton.FindBone("head");
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
			skeletonAnimation.state.SetAnimation(1, "shoot", false);
		}

		//ATTACK
		if (device.GetAction3DownOnce())
		{
			combo++; if (combo > 3)combo = 1;
			skeletonAnimation.state.SetAnimation(0, "attack" + combo, false);
		}

	}

	void FIRE_GUN(Spine.AnimationState state, int trackIndex, Spine.Event e)
	{
		weaponmanager.Fire();
	}

	void ATTACK_HIT(Spine.AnimationState state, int trackIndex, Spine.Event e)
	{
		Debug.Log("HEY");
	}

	void render (float delta) {
		Debug.Log("BO");
	}
}

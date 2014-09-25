﻿using UnityEngine;
using System.Collections;
using InControl;
using Spine;

public class PlayerControl : MonoBehaviour {

	PlayerDevice device;
	Movement movement;
	UnitInfo unitinfo;
	bool isMoving = false;
	int combo = 0;
	int gunKickBack = 0;
	string state = "idle";
	//[HideInInspector]
	WeaponManager weaponmanager;
	MeleeManager meleemanager;

	Bone head;
	Bone hip;
	SkeletonAnimation skeletonAnimation;

	public void SetDevice( PlayerDevice device )
	{
		this.device = device;
		unitinfo = gameObject.GetComponent<UnitInfo>();
		movement = gameObject.GetComponent<Movement>();
		skeletonAnimation = gameObject.GetComponentInChildren<SkeletonAnimation>();
		skeletonAnimation.state.Event += Event;
		skeletonAnimation.UpdateBones += UpdateBones;
		skeletonAnimation.state.End += End;
		head = gameObject.GetComponentInChildren<SkeletonAnimation>().skeleton.FindBone("head");
		hip = gameObject.GetComponentInChildren<SkeletonAnimation>().skeleton.FindBone("body");
		weaponmanager = gameObject.GetComponentInChildren<WeaponManager>();
		meleemanager = gameObject.GetComponentInChildren<MeleeManager>();
	}

	
	void Update ()
	{
		if (device == null)return;

		switch(state)
		{
			case "idle":
				if (skeletonAnimation.state.ToString() != "Idle"){skeletonAnimation.state.SetAnimation(0, "Idle", true);}
					canMove();
					canShoot();
					canJump();
					canAttack();
				break;
			case "run":
				if (skeletonAnimation.state.ToString() != "Run"){skeletonAnimation.state.SetAnimation(0, "Run", true);}
					canMove();
					canShoot();
					canJump();
					canAttack();	
				break;
			case "jump":
				canMove();
				canShoot();
				if (gameObject.rigidbody2D.velocity.y > 0)
				{
				if (skeletonAnimation.state.ToString() != "Jump" && skeletonAnimation.state.ToString() != "<none>")skeletonAnimation.state.SetAnimation(0, "Jump", false);
				} else { 
				if (skeletonAnimation.state.ToString() != "Fall" && skeletonAnimation.state.ToString() != "<none>")skeletonAnimation.state.SetAnimation(0, "Fall", false);
				}
				if (movement.onGround == true) 
				{
					if (device.GetInputMoveVector().x != 0f)
					{
						state = "run";
					} else { 
						state = "land";
						skeletonAnimation.state.SetAnimation(0, "Land", false);
						skeletonAnimation.state.AddAnimation(0, "Idle" , true, 0f);
					}
				}
			break;
			case "land":
				
			//	if (skeletonAnimation.state.ToString() != "Land")skeletonAnimation.state.SetAnimation(0, "Land", false);
				//wait until animation has played
			break;
			case "attack":
				Vector2 velocity = gameObject.rigidbody2D.velocity;
				velocity.x += (0f - velocity.x)/8;
				gameObject.rigidbody2D.velocity = velocity;
			break;
		}

		//Off ground animation
		if (!movement.onGround)
		{
			state = "jump";
		}
	}


	void canMove(){
		Vector2 inputMoveVector = device.GetInputMoveVector();
		movement.Move(unitinfo.MoveSpeed * inputMoveVector.x);
		if (inputMoveVector.x != 0.0f)
		{
			if (!isMoving)
			{
				if (movement.onGround == true)state = "run";
			}
			isMoving = true;
			
		} else {
			if (isMoving == true)
			{
				if (movement.onGround == true)state = "idle";
			}
			isMoving = false;
		}
		skeletonAnimation.transform.localScale = movement.facing;
	}


	void canShoot()
	{
		if (device.GetAction2Down())
		{
			if (Time.time > weaponmanager.CurrentWeapon.wait)
			{
				skeletonAnimation.state.SetAnimation(1, "shoot", false);
			}
		}
	}

	void canAttack()
	{
		//ATTACK
		if (device.GetAction3DownOnce())
		{
			combo++; if (combo > 3)combo = 1;
			Vector2 velocity = gameObject.rigidbody2D.velocity;
			velocity.x = 10.0f * skeletonAnimation.transform.localScale.x;
			gameObject.rigidbody2D.velocity = velocity;
			skeletonAnimation.state.SetAnimation(0, "attack" + combo, false);
			skeletonAnimation.state.AddAnimation(0, "Idle" , true, 0f);
			state = "attack";
		}
	}

	void canJump()
	{
		if (device.GetAction1DownOnce())
		{
			movement.Jump(unitinfo.JumpHeight);
		}
	}



	void Event(Spine.AnimationState state, int trackIndex, Spine.Event e)
	{
		switch (e.ToString())
		{
			case "ATTACK_HIT":
				Debug.Log ("SWING!");
				meleemanager.Attack();
			break;
			case "FIRE_GUN":
			Bone gun = gameObject.GetComponentInChildren<SkeletonAnimation>().skeleton.FindBone("gun");
			weaponmanager.transform.position = new Vector3(transform.position.x + (gun.worldX * skeletonAnimation.transform.localScale.x), transform.position.y + gun.WorldY);
			weaponmanager.Fire(); gunKickBack = 20;
			break;
		}
	}

	void End(Spine.AnimationState animState, int trackIndex) {
		switch (animState.ToString())
		{
			case "Land":
				if (state == "land")
				{
					state = "idle";
				}
			break;

		case "attack1":
		case "attack2": 
		case "attack3":
				if (state == "attack")
				{
					if (device.GetInputMoveVector().x != 0f)
					{
						state = "run"; 
					} else {
						state = "idle";
					}
				}
			break;
		}
	}

	void UpdateBones(SkeletonAnimation skeletonAnimation) {
		/*
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
		tempRot = Mathf.Clamp(tempRot, LowerRotationBound, UpperRotationBound);
		if (tempRot < UpperRotationBound && tempRot > LowerRotationBound) head.Rotation = tempRot;
		*/
		hip.Rotation += gunKickBack; if (gunKickBack > 0) gunKickBack--;
	}
}

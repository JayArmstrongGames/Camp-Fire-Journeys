using UnityEngine;
using System.Collections;
using InControl;
using Spine;

public class PlayerControl : MonoBehaviour {

	PlayerDevice device;
	Movement movement;
	UnitStats unitstats;
	LevelInfo level;
	bool isMoving = false;
	int combo = 0;
	int gunKickBack = 0;
	string state = "idle";
	bool sliding = true;
	bool moving = true;
	public 	SimpleTrigger rwallslide;
	public 	SimpleTrigger lwallslide;
	public 	SimpleTrigger rwallcling;
	public 	SimpleTrigger lwallcling;

	//[HideInInspector]
	WeaponManager weaponmanager;
	MeleeManager meleemanager;

	Bone head;
	Bone hip;
	SkeletonAnimation skeletonAnimation;

	public void SetDevice( PlayerDevice device )
	{
		this.device = device;
		unitstats = gameObject.GetComponent<UnitStats>();
		movement = gameObject.GetComponent<Movement>();
		skeletonAnimation = gameObject.GetComponentInChildren<SkeletonAnimation>();
		skeletonAnimation.state.Event += Event;
		skeletonAnimation.UpdateBones += UpdateBones;
		skeletonAnimation.state.End += End;
		head = gameObject.GetComponentInChildren<SkeletonAnimation>().skeleton.FindBone("head");
		hip = gameObject.GetComponentInChildren<SkeletonAnimation>().skeleton.FindBone("body");
		weaponmanager = gameObject.GetComponentInChildren<WeaponManager>();
		meleemanager = gameObject.GetComponentInChildren<MeleeManager>(); 
		GameObject gameGameObject = GameObject.FindGameObjectWithTag( "Level" );
		level = gameGameObject.GetComponent<LevelInfo>(); 
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
				if (canSlide()){return;}
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
			case "chargingAttack":
				if (device.GetInputMoveVector().x < 0)
				{
					Vector3 turnAround = movement.facing; turnAround.x = -1; movement.facing = turnAround;
					skeletonAnimation.transform.localScale = movement.facing;
				} else if (device.GetInputMoveVector().x > 0)
				{
					Vector3 turnAround = movement.facing; turnAround.x = 1; movement.facing = turnAround;
					skeletonAnimation.transform.localScale = movement.facing;
				}
				canAttack();
			break;
			case "attack":
				Vector2 velocity = gameObject.rigidbody2D.velocity;
				velocity.x += (0f - velocity.x)/8;
				gameObject.rigidbody2D.velocity = velocity;
			break;
			case "chargeAttack":
				velocity = gameObject.rigidbody2D.velocity;
				if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
				{
					velocity.x += (0f - velocity.x)/15;
					velocity.y = 0f;
					if (Mathf.Abs(velocity.x) < 3f)state = "idle";
				} else {
					velocity.x = 0f;
					velocity.y += (0f - velocity.y)/15;
					if (velocity.y < 1f)state = "idle";
			}
				gameObject.rigidbody2D.velocity = velocity;
			break;
			case "wallslide":
				Vector2 slide = gameObject.rigidbody2D.velocity;
				slide.y += (0.5f - slide.y) / 4;
				gameObject.rigidbody2D.velocity = slide;
				if (skeletonAnimation.state.ToString() != "Wallslide" || skeletonAnimation.state.ToString() != "<none>")skeletonAnimation.state.SetAnimation(0, "Wallslide", true);


				if ((rwallslide.IsColliding && device.GetInputMoveVector().x <= 0) || (lwallslide.IsColliding && device.GetInputMoveVector().x >= 0))
				{
				state = "jump"; 
				}
				if (!rwallslide.IsColliding && !lwallslide.IsColliding)
				{
				state = "jump";
				}
				
				if (canJump())
				{
					slide = gameObject.rigidbody2D.velocity;
					slide.x = 5f * movement.facing.x;
					slide.y = movement.JumpHeight;
					gameObject.rigidbody2D.velocity = slide;
					state = "jump";
					moveDelay();
				}

			break;
			case "cling":
				
				gameObject.rigidbody2D.Sleep();
				if (skeletonAnimation.state.ToString() != "Cling")skeletonAnimation.state.SetAnimation(0, "Cling", true);
				
				if (canJump())
				{
					gameObject.rigidbody2D.IsAwake();
					slide = gameObject.rigidbody2D.velocity;
					slide.y = movement.JumpHeight;
					gameObject.rigidbody2D.velocity = slide;
					movement.Jump(movement.JumpHeight);
					state = "jump";
					clingDelay();
				}
				if ((rwallslide.IsColliding && device.GetInputMoveVector().x <= 0) || (lwallslide.IsColliding && device.GetInputMoveVector().x >= 0))
				{
					gameObject.rigidbody2D.IsAwake();
					slide = gameObject.rigidbody2D.velocity;
					slide.x = 5f * movement.facing.x;
					gameObject.rigidbody2D.velocity = slide;
					state = "jump";
					clingDelay();
				}
			break;
	
		}

		if (canRegainMove && !moving)
		{
			if (device.GetInputMoveVector().x != 0f || movement.onGround)
			{
				moving = true;
			}
		}
		if (!movement.onGround && state != "wallslide" && state != "cling" && state != "chargeAttack")
		{
			state = "jump";
		}
	}

	void moveDelay(float delay = 0.4f)
	{
		canRegainMove = false;
		moving = false;
		Invoke("enableMove", delay);
	}
	bool canRegainMove;
	void enableMove()
	{
		canRegainMove = true;
	}

	void canMove(){
		if (!moving)return;
		Vector2 inputMoveVector = device.GetInputMoveVector();
		movement.Move(movement.MoveSpeed * inputMoveVector.x);
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

	float attackCharge = 0f;
	void canAttack()
	{
		if (device.GetAction3DownOnce())
		{
			attackCharge = Time.time;
		}

		if (device.GetAction3Down())
		{
			if (Time.time - attackCharge > 0.2f && state != "chargingAttack")
			{
				state = "chargingAttack";
				skeletonAnimation.state.SetAnimation(0, "Charging", true);
			}
		}

		if (device.GetAction3UpOnce())
		{
			if (Time.time - attackCharge > 0.3f)
			{
				doChargeAttack();
			} else {
				doAttack();
			}
			attackCharge = 0f;
		}
	}

	void doAttack(){
		Debug.Log("Standard attack");
		//ATTACK
			float yThrust = device.GetInputMoveVector().y; Debug.Log ("Y THURST " + yThrust);
			if (yThrust > 0){ 
				skeletonAnimation.state.SetAnimation(0, "attackUp", false);
			} else {
				combo++; if (combo > 3)combo = 1;
				gameObject.rigidbody2D.velocity = new Vector2(10f * skeletonAnimation.transform.localScale.x, gameObject.rigidbody2D.velocity.y);
				skeletonAnimation.state.SetAnimation(0, "attack" + combo, false);
			}
			skeletonAnimation.state.AddAnimation(0, "Idle" , true, 0f);

			state = "attack";
	}

	void doChargeAttack(){
		Debug.Log("Charge attack");
		//ATTACK
		combo++; if (combo > 3)combo = 1;
		Vector2 velocity = gameObject.rigidbody2D.velocity;
		if (device.GetInputMoveVector().y != 0f)
		{
			velocity.y = 26f;
			velocity.x = 0f;
			skeletonAnimation.state.SetAnimation(0, "Charge_vertical", false);
		} else {
			velocity.x = 20.0f * skeletonAnimation.transform.localScale.x;
			velocity.y = 0f;
			skeletonAnimation.state.SetAnimation(0, "Charge_horizontal", false);
		}
		gameObject.rigidbody2D.velocity = velocity;

		skeletonAnimation.state.AddAnimation(0, "Idle" , true, 0f);
		state = "chargeAttack";
	}

	bool canJump()
	{
		if (device.GetAction1DownOnce())
		{
			movement.Jump(movement.JumpHeight);
			return true;
		}
		return false;
	}



	void clingDelay(float delay = 0.3f)
	{
		sliding = false;
		Invoke("enableCling", delay);
	}
	void enableCling()
	{
		sliding = true;
	}
	
	bool canSlide()
	{
		if (!sliding)return false;
	
		if ((rwallslide.IsColliding && device.GetInputMoveVector().x > 0) || (lwallslide.IsColliding && device.GetInputMoveVector().x < 0))
		{
			int xpos; int ypos;

				//Dont wall slide if nothing on bottom;
				if ( rwallslide.IsColliding && device.GetInputMoveVector().x > 0)
				{
					level.tileMap.GetTileAtPosition( transform.position, out  xpos, out  ypos);
					if (level.tileMap.GetTile(xpos + 1, ypos, 1) != -1 && level.tileMap.GetTile(xpos + 1, ypos + 1, 1) != -1)
					{
						Vector3 turnAround = movement.facing; 
						turnAround.x = -1; 
						movement.facing = turnAround;
						skeletonAnimation.transform.localScale = movement.facing;
						state = "wallslide";
						return true;
					}
				}

				if (lwallslide.IsColliding && device.GetInputMoveVector().x < 0)
				{
					level.tileMap.GetTileAtPosition( transform.position, out  xpos, out  ypos);
					if (level.tileMap.GetTile(xpos - 1, ypos, 1) != -1 && level.tileMap.GetTile(xpos - 1, ypos + 1, 1) != -1)
					{
						Vector3 turnAround = movement.facing; 
						turnAround.x = 1; 
						movement.facing = turnAround;
						skeletonAnimation.transform.localScale = movement.facing;
						state = "wallslide";
						return true;
					}
				}
			


			//CLINGING
			if ( (rwallslide.IsColliding && !rwallcling.IsColliding) || (lwallslide.IsColliding && !lwallcling.IsColliding))
			{
				Vector3 turnAround = movement.facing; 
				if (rwallslide.IsColliding) turnAround.x = 1; 
				if (lwallslide.IsColliding) turnAround.x = -1; 
				movement.facing = turnAround;
				skeletonAnimation.transform.localScale = movement.facing;

				//Lock to edge

				Vector3 lockToEdge = gameObject.transform.position;
				level.tileMap.GetTileAtPosition( transform.position, out  xpos, out  ypos);
				if (level.tileMap.GetTile(xpos, ypos - 1, 1) != -1)return false;
				if ((lwallslide.IsColliding && !lwallcling.IsColliding))
				{
					level.tileMap.GetTileAtPosition( lwallslide.transform.position, out  xpos, out  ypos);
					lockToEdge.x = level.tileMap.GetTilePosition(xpos , ypos).x + 0.3f;
					lockToEdge.y = level.tileMap.GetTilePosition(xpos , ypos).y + 0.5f;
				}
				if ((rwallslide.IsColliding && !rwallcling.IsColliding))
				{
					level.tileMap.GetTileAtPosition( rwallslide.transform.position, out  xpos, out  ypos);
					lockToEdge.x = level.tileMap.GetTilePosition(xpos + 1, ypos).x - 0.3f;
					lockToEdge.y = level.tileMap.GetTilePosition(xpos + 1, ypos).y + 0.5f;
				}
					
				gameObject.transform.position = lockToEdge;

				state = "cling";
				return true;
			}

		}
		return false;
	}

	void Event(Spine.AnimationState state, int trackIndex, Spine.Event e)
	{
		Debug.Log("EVENT: " +e.ToString());
		switch (e.ToString())
		{
			case "ATTACK_HIT":
			if (device.GetInputMoveVector().y > 0)
			{
				meleemanager.Attack("UP");
			} else {
				meleemanager.Attack();
			}
			break;
			case "CHARGE_VERTICAL_HIT":
				meleemanager.Attack();
			break;
			case "CHARGE_HORIZONTAL_HIT":
				meleemanager.Attack("CHARGEUP");
			break;
			case "FIRE_GUN":
			Bone gun = gameObject.GetComponentInChildren<SkeletonAnimation>().skeleton.FindBone("GunEnd");
			weaponmanager.transform.position = new Vector3(transform.position.x + (gun.worldX * skeletonAnimation.transform.localScale.x), transform.position.y + gun.WorldY - 0.7f);
			Debug.Log("wpnmanager facing1 " + weaponmanager.transform.localScale);
			Vector3 gunFacing = weaponmanager.transform.localScale;gunFacing.x = skeletonAnimation.transform.localScale.x;weaponmanager.transform.localScale = gunFacing;
			Debug.Log("wpnmanager facing2 " + weaponmanager.transform.localScale);
			weaponmanager.Fire(); gunKickBack = 20;
			break;
		}
	}

	void End(Spine.AnimationState animState, int trackIndex) {

		if (animState.ToString().Contains("Land") == true)
		{
			if (state == "land")
			{
				state = "idle";
			}
		}

		switch (animState.ToString())
		{
		case "attack1":
		case "attack2": 
		case "attack3":
		case "Charge_horizontal":
		case "Charge_vertical":
		case "attackUp":
			if (state == "attack" || state == "chargeAttack")
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

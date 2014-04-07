using UnityEngine;
using System.Collections;
using InControl;

public class PlayerControl : MonoBehaviour {

	PlayerDevice device;
	Movement movement;
	UnitInfo unitinfo;
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

		Vector2 inputMoveVector = device.GetInputMoveVector();

		movement.Move(unitinfo.MoveSpeed * inputMoveVector.x);

		if (device.GetAction1DownOnce())
		{
			movement.Jump(unitinfo.JumpHeight);
		}

		if (device.GetAction2Down())
		{
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
}

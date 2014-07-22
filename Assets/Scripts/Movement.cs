using UnityEngine;
using System.Collections;


public class Movement : MonoBehaviour {

	public SimpleTrigger lWall;
	public SimpleTrigger rWall;
	public SimpleTrigger floorTrigger;

	public float direction = 1;
	bool onGround;

	void Start()
	{
		Vector3 facing = gameObject.transform.localScale;
		if (direction != facing.x)
		{
			facing.x = direction;
			SpriteRenderer[] spriterenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
			for (int i = 0; i < spriterenderers.Length; i++)
			{
				spriterenderers[i].transform.localScale = facing;
			}

			SkeletonAnimation[] skeletons = gameObject.GetComponentsInChildren<SkeletonAnimation>();
			for (int i = 0; i < skeletons.Length; i++)
			{
				skeletons[i].transform.localScale = facing;
			}
		}

	}

	public void Move(float speed)
	{
		if ( (rWall.IsColliding && !onGround && speed > 0) || (lWall.IsColliding && !onGround && speed < 0)) return;

		Vector2 velocity = gameObject.rigidbody2D.velocity;
		velocity.x = speed;
		gameObject.rigidbody2D.velocity = velocity;

		if (speed == 0)return;
		Vector3 facing = gameObject.transform.localScale;
		if (speed < 0 )
		{
			facing.x = -1;
		}
		if (speed > 0 )
		{
			facing.x = 1;
		}
		if (direction != facing.x)
		{
			direction = facing.x;
			SpriteRenderer[] spriterenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
			for (int i = 0; i < spriterenderers.Length; i++)
			{
				spriterenderers[i].transform.localScale = facing;
			}
			SkeletonAnimation[] skeletons = gameObject.GetComponentsInChildren<SkeletonAnimation>();
			for (int i = 0; i < skeletons.Length; i++)
			{
				skeletons[i].transform.localScale = facing;
				Debug.Log ("skele"+ skeletons[i]);
				skeletons[i].state.SetAnimation(0, "walk", true);

			}
		}

		//Vector3 wallTriggerFacing = wallTrigger.transform.localPosition;
		//wallTriggerFacing.x = 0.5f * facing.x;
		//if (wallTriggerFacing != wallTrigger.transform.localPosition)
		//{
		//	wallTrigger.clearColliders();
		//}
		//wallTrigger.transform.localPosition = wallTriggerFacing;

	}

	public void Jump(float jumpheight)
	{

		if (!onGround) return;
		Vector3 velocity = gameObject.rigidbody2D.velocity;
		velocity.y = jumpheight;
		gameObject.rigidbody2D.velocity = velocity;
	}

	void Update()
	{

		if (!floorTrigger.IsColliding )
		{ onGround = false; } else { onGround = true; }
	}

}

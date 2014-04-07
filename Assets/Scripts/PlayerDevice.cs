using InControl;
using UnityEngine;

public class PlayerDevice
{
	public InputDevice device;
	public InputDevice altDevice;

	public PlayerDevice( InputDevice device, InputDevice altDevice = null )
	{
		this.device = device;
		this.altDevice = altDevice;
	}

	public virtual Vector2 GetInputMoveVector()
	{
		if ( altDevice == null ) return device.Direction;
		else
		{
			if( device.LastChangeTime > altDevice.LastChangeTime )
			{
				return device.Direction;
			}
			else
			{
				return altDevice.Direction;
			}
		}
	}

	public virtual bool GetAction1DownOnce()
	{
		if ( altDevice == null ) return device.Action1.WasPressed;
		else
		{
			return device.Action1.WasPressed | altDevice.Action1.WasPressed;
		}
	}

	public virtual bool GetAction1Down()
	{
		if ( altDevice == null ) return device.Action1.IsPressed;
		else
		{
			return device.Action1.IsPressed | altDevice.Action1.IsPressed;
		}
	}

	public virtual bool GetAction1UpOnce()
	{
		if ( altDevice == null ) return device.Action1.WasReleased;
		else
		{
			return device.Action1.WasReleased | altDevice.Action1.WasReleased;
		}
	}

	public virtual bool GetAction2DownOnce()
	{
		if ( altDevice == null ) return device.Action2.WasPressed;
		else
		{
			return device.Action2.WasPressed | altDevice.Action2.WasPressed;
		}
	}

	public virtual bool GetAction2Down()
	{
		if ( altDevice == null ) return device.Action2.IsPressed;
		else
		{
			return device.Action2.IsPressed | altDevice.Action2.IsPressed;
		}
	}

	public virtual bool GetAction2UpOnce()
	{
		if ( altDevice == null ) return device.Action2.WasReleased;
		else
		{
			return device.Action2.WasReleased | altDevice.Action2.WasReleased;
		}
	}


	public virtual bool GetAction3DownOnce()
	{
		if ( altDevice == null ) return device.Action3.WasPressed;
		else
		{
			return device.Action3.WasPressed | altDevice.Action3.WasPressed;
		}
	}
	
	public virtual bool GetAction3Down()
	{
		if ( altDevice == null ) return device.Action3.IsPressed;
		else
		{
			return device.Action3.IsPressed | altDevice.Action3.IsPressed;
		}
	}
	
	public virtual bool GetAction3UpOnce()
	{
		if ( altDevice == null ) return device.Action3.WasReleased;
		else
		{
			return device.Action3.WasReleased | altDevice.Action3.WasReleased;
		}
	}


	public virtual bool GetAction4DownOnce()
	{
		if ( altDevice == null ) return device.Action4.WasPressed;
		else
		{
			return device.Action4.WasPressed | altDevice.Action4.WasPressed;
		}
	}

	public virtual bool GetAction4Down()
	{
		if ( altDevice == null ) return device.Action4.IsPressed;
		else
		{
			return device.Action4.IsPressed | altDevice.Action4.IsPressed;
		}
	}

	public virtual bool GetAction4UpOnce()
	{
		if ( altDevice == null ) return device.Action4.WasReleased;
		else
		{
			return device.Action4.WasReleased | altDevice.Action4.WasReleased;
		}
	}

	public virtual void Vibrate( float intensity )
	{
		device.Vibrate( intensity );
		if ( altDevice != null ) altDevice.Vibrate( intensity );
	}

	// Are any inputs being processed by this device
	public virtual bool IsActive()
	{
		return GetAction1Down() | GetAction2Down() | GetAction3Down() | GetAction4Down() | GetInputMoveVector().x > 0.0f | GetInputMoveVector().y > 0.0f;
	}
}

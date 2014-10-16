using InControl;
using System.Collections.Generic;
using UnityEngine;

public class Main
{
	private static Main instance;

	public static Main GetInstance()
	{
		if ( instance == null )
			instance = new Main();
		return instance;
	}

	public List<PlayerDevice> playerDevices;
	List<InputDevice> usedDevices;

	public Main()
	{
		if( instance != null )
		{
			Debug.LogError( "Cannot create a new instance of Main. Main is a singleton." );
			return;
		}

		//InputManager.EnableXInput = true;
		InputManager.Setup();

		InputDevice keyboardAndMouse = new UnityInputDevice( new KeyboardMouseDeviceProfile() );
		InputManager.AttachDevice( keyboardAndMouse );

		playerDevices = new List<PlayerDevice>();
		usedDevices = new List<InputDevice>();

		for ( int i = 0; i < InputManager.Devices.Count; i++ )
		{
			Debug.Log( InputManager.Devices[i].Name );

			if( !usedDevices.Contains(InputManager.Devices[i]) )
			{
				if( playerDevices.Count == 0 )
				{
					usedDevices.Add( InputManager.Devices[i] );
					usedDevices.Add( keyboardAndMouse );
					playerDevices.Add( new PlayerDevice( InputManager.Devices[i], keyboardAndMouse ) );
				}
				else
				{
					usedDevices.Add( InputManager.Devices[i] );
					playerDevices.Add( new PlayerDevice( InputManager.Devices[i] ) );
				}
			}
		}

		InputManager.OnDeviceAttached += HandleDeviceAttached;
		InputManager.OnDeviceDetached += HandlDeviceDetached;
	}

	private void HandleDeviceAttached( InputDevice device )
	{
		Debug.Log( "Attached: " + device.Name + " " + device.Meta + "   ");
	}

	private void HandlDeviceDetached( InputDevice device )
	{
		Debug.Log( "Detached: " + device.Name + " " + device.Meta );
	}

	public void Update()
	{
		InputManager.Update();
	}

	public int TotalDevicesPluggedIn()
	{
		return InputManager.Devices.Count;
	}
}

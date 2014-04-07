using InControl;
using UnityEngine;

public class KeyboardMouseDeviceProfile : UnityInputDeviceProfile
{
	public KeyboardMouseDeviceProfile()
	{
		Name = "Keyboard/Mouse";
		Meta = "A keyboard and mouse combination profile appropriate";

		SupportedPlatforms = new[]
		{
			"Windows",
			"Mac",
			"Linux"
		};

		Sensitivity = 1.0f;
		LowerDeadZone = 0.0f;

		ButtonMappings = new[]
		{
			new InputControlMapping
			{
				Handle = "W",
				Target = InputControlType.Action1,
				Source = KeyCodeButton( KeyCode.W )
			},
			new InputControlMapping
			{
				Handle = "SPACE",
				Target = InputControlType.Action2,
				Source = KeyCodeButton( KeyCode.Space )
			},
			new InputControlMapping
			{
				Handle = "MouseButton1",
				Target = InputControlType.Action3,
				Source = MouseButton1
			},
			new InputControlMapping
			{
				Handle = "E",
				Target = InputControlType.Action4,
				Source = KeyCodeButton( KeyCode.E )
			}
		};

		AnalogMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "Move X",
					Target = InputControlType.LeftStickX,
					Source = KeyCodeAxis( KeyCode.A, KeyCode.D )
				},
				new InputControlMapping
				{
					Handle = "Move Y",
					Target = InputControlType.LeftStickY,
					Source = KeyCodeAxis( KeyCode.S, KeyCode.W )
				},
				new InputControlMapping
				{
					Handle = "Look X",
					Target = InputControlType.RightStickX,
					Source = MouseXAxis,
					Raw    = true
				},
				new InputControlMapping
				{
					Handle = "Look Y",
					Target = InputControlType.RightStickY,
					Source = MouseYAxis,
					Raw    = true
				}
			};
	}
}

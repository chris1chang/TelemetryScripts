using UnityEngine;
using System.Collections;

public class MouseCameraControlP6_curvedsetup : MonoBehaviour {
	// Mouse buttons in the same order as Unity
	public enum MouseButton { Left = 0, Right = 1, Middle = 2, None = 3 }
	
	[System.Serializable]
	// Handles left modifiers keys (Alt, Ctrl, Shift)
	public class Modifiers
	{
		public bool leftAlt;
		public bool leftControl;
		public bool leftShift;
		
		public bool checkModifiers()
		{
			return (!leftAlt ^ Input.GetKey(KeyCode.LeftAlt)) &&
				(!leftControl ^ Input.GetKey(KeyCode.LeftControl)) &&
					(!leftShift ^ Input.GetKey(KeyCode.LeftShift));
		}
	}
	
	[System.Serializable]
	// Handles common parameters for translations and rotations
	public class MouseControlConfiguration
	{
		
		public bool activate;
		public MouseButton mouseButton;
		public Modifiers modifiers;
		public float sensitivity;
		
		public bool isActivated()
		{
			return activate && Input.GetMouseButton((int)mouseButton) && modifiers.checkModifiers();
		}
	}
	
	[System.Serializable]
	// Handles scroll parameters
	public class MouseScrollConfiguration
	{
		
		public bool activate;
		public Modifiers modifiers;
		public float sensitivity;
		
		public bool isActivated()
		{
			return activate && modifiers.checkModifiers();
		}
	}
	
	// Yaw default configuration
	public MouseControlConfiguration yaw = new MouseControlConfiguration { mouseButton = MouseButton.Right, sensitivity = 10F };
	
	// Pitch default configuration
	public MouseControlConfiguration pitch = new MouseControlConfiguration { mouseButton = MouseButton.Right, modifiers = new Modifiers{ leftControl = true }, sensitivity = 10F };
	
	// Roll default configuration
	public MouseControlConfiguration roll = new MouseControlConfiguration();
	
	// Vertical translation default configuration
	public MouseControlConfiguration verticalTranslation = new MouseControlConfiguration { mouseButton = MouseButton.Middle, sensitivity = 2F };
	
	// Horizontal translation default configuration
	public MouseControlConfiguration horizontalTranslation = new MouseControlConfiguration { mouseButton = MouseButton.Middle, sensitivity = 2F };
	
	// Depth (forward/backward) translation default configuration
	public MouseControlConfiguration depthTranslation = new MouseControlConfiguration { mouseButton = MouseButton.Left, sensitivity = 2F };
	
	// Scroll default configuration
	public MouseScrollConfiguration scroll = new MouseScrollConfiguration { sensitivity = 2F };
	
	// Default unity names for mouse axes
	public string mouseHorizontalAxisName = "Mouse X";
	public string mouseVerticalAxisName = "Mouse Y";
	public string scrollAxisName = "Mouse ScrollWheel";
	
	void Start ()
	{
		float translateY = Input.GetAxis(mouseVerticalAxisName) * verticalTranslation.sensitivity;
		float translateX = Input.GetAxis(mouseHorizontalAxisName) * horizontalTranslation.sensitivity;
		float translateZ = Input.GetAxis(mouseVerticalAxisName) * depthTranslation.sensitivity;
	}

	void LateUpdate ()
	{
		var Monitor1 = GameObject.Find("Monitor 1");
		var Monitor2 = GameObject.Find("Monitor 2");
		var Monitor3 = GameObject.Find("Monitor 3");
		var Monitor4 = GameObject.Find("Monitor 4");
		var Monitor5 = GameObject.Find("Monitor 5");


		if (Input.GetKey(KeyCode.Space))
			Screen.lockCursor = true;
		else
			Screen.lockCursor = false;

		//Follow mouse vertical and horizontal
		float translateY = Input.GetAxis(mouseVerticalAxisName) * verticalTranslation.sensitivity;
		float translateX = Input.GetAxis(mouseHorizontalAxisName) * horizontalTranslation.sensitivity;
		float translateZ = Input.GetAxis(mouseVerticalAxisName) * depthTranslation.sensitivity;

		transform.Translate(new Vector3(0, translateY, 0));	
		transform.Translate(new Vector3(translateX, 0, 0));
		transform.Translate(new Vector3(0, 0, translateZ));
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, (Monitor5.transform.renderer.bounds.min.x + 0.05f), (Monitor1.transform.renderer.bounds.max.x - 0.05f)), Mathf.Clamp(transform.position.y, Monitor3.transform.renderer.bounds.min.y + 0.01f, Monitor3.transform.renderer.bounds.max.y - 0.09f),Mathf.Clamp(transform.position.z, Monitor2.transform.renderer.bounds.max.z, Monitor1.transform.renderer.bounds.max.z));


		if((transform.position.x >= Monitor1.transform.renderer.bounds.min.x) && (transform.position.x <= Monitor1.transform.renderer.bounds.max.x))
		{
			//IN MONITOR 1
			float thickness = Monitor3.transform.renderer.bounds.max.z - Monitor3.transform.renderer.bounds.min.z;
			float M1_z = Monitor1.transform.renderer.bounds.max.z - Monitor1.transform.renderer.bounds.min.z - thickness;
			float M1_x = Monitor1.transform.renderer.bounds.max.x - Monitor1.transform.renderer.bounds.min.x;
			float M1_slope = M1_z/M1_x;
			transform.position = new Vector3(transform.position.x, transform.position.y, ((transform.position.x)*M1_slope) - 6.139584f);

		}

		if((transform.position.x >= Monitor2.transform.renderer.bounds.max.x) && (transform.position.x <= Monitor1.transform.renderer.bounds.min.x)) 
		{
			//FROM MONITOR 2 TO MONITOR 1 OR MONITOR  1 TO MONITOR 2
			if (translateX < 0)
			{
				transform.position = new Vector3 (Monitor2.transform.renderer.bounds.max.x, transform.position.y, transform.position.z);
			}
			if (translateX > 0)
			{
				transform.position = new Vector3 (Monitor1.transform.renderer.bounds.min.x, transform.position.y, transform.position.z);
			}
		}
		
		if((transform.position.x >= Monitor2.transform.renderer.bounds.min.x) && (transform.position.x <= Monitor2.transform.renderer.bounds.max.x))
		{
			//MONITOR 2

			float thickness = Monitor3.transform.renderer.bounds.max.z - Monitor3.transform.renderer.bounds.min.z;
			float M2_z = Monitor2.transform.renderer.bounds.max.z - Monitor2.transform.renderer.bounds.min.z - thickness;
			float M2_x = Monitor2.transform.renderer.bounds.max.x - Monitor2.transform.renderer.bounds.min.x;
			float M2_slope = M2_z/M2_x;
			transform.position = new Vector3(transform.position.x, transform.position.y, ((transform.position.x)*M2_slope) - 3.9007234f);
		}

		if((transform.position.x >= Monitor3.transform.renderer.bounds.max.x) && (transform.position.x <= Monitor2.transform.renderer.bounds.min.x)) 
		{
			//FROM MONITOR 2 TO MONITOR 3 OR MONITOR  3 TO MONITOR 2
			if (translateX < 0)
			{
				transform.position = new Vector3 (Monitor3.transform.renderer.bounds.max.x, transform.position.y, transform.position.z);
			}
			if (translateX > 0)
			{
				transform.position = new Vector3 (Monitor2.transform.renderer.bounds.min.x, transform.position.y, transform.position.z);
			}
		}

		if((transform.position.x >= Monitor3.transform.renderer.bounds.min.x) && (transform.position.x <= Monitor3.transform.renderer.bounds.max.x))
		{
			//IN MONITOR 3
			transform.position = new Vector3(transform.position.x, transform.position.y, Monitor3.transform.renderer.bounds.max.z);
		}

		if((transform.position.x <= Monitor3.transform.renderer.bounds.min.x) && (transform.position.x >= Monitor4.transform.renderer.bounds.max.x)) 
		{
			//FROM 3 TO 4 OR 4 TO 3
			if (translateX < 0)
			{
				transform.position = new Vector3 (Monitor4.transform.renderer.bounds.max.x, transform.position.y, Mathf.Clamp(transform.position.z, transform.position.z, Monitor4.transform.renderer.bounds.max.z));
			}
			if (translateX > 0)
			{
				transform.position = new Vector3 (Monitor3.transform.renderer.bounds.min.x, transform.position.y, transform.position.z);

			}
		}

		if((transform.position.x >= Monitor4.transform.renderer.bounds.min.x) && (transform.position.x <= Monitor4.transform.renderer.bounds.max.x))
		{
			//IN MONITOR 4
			float thickness = Monitor3.transform.renderer.bounds.max.z - Monitor3.transform.renderer.bounds.min.z;
			float M4_z = Monitor4.transform.renderer.bounds.max.z - Monitor4.transform.renderer.bounds.min.z - thickness;
			float M4_x = Monitor4.transform.renderer.bounds.max.x - Monitor4.transform.renderer.bounds.min.x;
			float M4_slope = M4_z/M4_x;
			transform.position = new Vector3(transform.position.x, transform.position.y, -((transform.position.x)*M4_slope) - 2.7275798f);
		}
		
				if((transform.position.x <= Monitor4.transform.renderer.bounds.min.x) && (transform.position.x >= Monitor5.transform.renderer.bounds.max.x)) 
		{
			//FROM 4 TO 5 OR 5 TO 4
			if (translateX < 0)
			{
				transform.position = new Vector3 (Monitor5.transform.renderer.bounds.max.x, transform.position.y, transform.position.z);
			}
			if (translateX > 0)
			{
				transform.position = new Vector3 (Monitor4.transform.renderer.bounds.min.x, transform.position.y, transform.position.z);

			}
		}
		
				if((transform.position.x >= Monitor5.transform.renderer.bounds.min.x) && (transform.position.x <= Monitor5.transform.renderer.bounds.max.x))
		{
			//IN MONITOR 5
			float thickness = Monitor3.transform.renderer.bounds.max.z - Monitor3.transform.renderer.bounds.min.z;
			float M5_z = Monitor5.transform.renderer.bounds.max.z - Monitor5.transform.renderer.bounds.min.z - thickness;
			float M5_x = Monitor5.transform.renderer.bounds.max.x - Monitor5.transform.renderer.bounds.min.x;
			float M5_slope = M5_z/M5_x;
			transform.position = new Vector3(transform.position.x, transform.position.y, -((transform.position.x)*M5_slope) - 2.633341f);

		}
	}
	
}
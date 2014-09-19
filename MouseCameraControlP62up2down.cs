using UnityEngine;
using System.Collections;

public class MouseCameraControlP62up2down : MonoBehaviour {
	
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
		var TopMonitor1 = GameObject.Find("Top Monitor 1");
		var TopMonitor2 = GameObject.Find("Top Monitor 2");
		var BotMonitor1 = GameObject.Find("Bottom Monitor 1");
		var BotMonitor2 = GameObject.Find("Bottom Monitor 2");

	//	print(Monitor1);
		if (Input.GetKey(KeyCode.Space))
			Screen.lockCursor = true;
		else
			Screen.lockCursor = false;
		/*  if (yaw.isActivated())
        {
            float rotationX = Input.GetAxis(mouseHorizontalAxisName) * yaw.sensitivity;
            transform.Rotate(0, rotationX, 0);
        }
        if (pitch.isActivated())
        {
            float rotationY = Input.GetAxis(mouseVerticalAxisName) * pitch.sensitivity;
            transform.Rotate(-rotationY, 0, 0);
        }
        if (roll.isActivated())
        {
            float rotationZ = Input.GetAxis(mouseHorizontalAxisName) * roll.sensitivity;
            transform.Rotate(0, 0, rotationZ);
        }
        
       // if (verticalTranslation.isActivated())
        //{
            float translateY = Input.GetAxis(mouseVerticalAxisName) * verticalTranslation.sensitivity;
            transform.Translate(0, translateY, 0);
        //}
 
        //if (horizontalTranslation.isActivated())
        //{
            float translateX = Input.GetAxis(mouseHorizontalAxisName) * horizontalTranslation.sensitivity;
            transform.Translate(translateX, 0, 0);
        //}
 
        if (depthTranslation.isActivated())
        {
            float translateZ = Input.GetAxis(mouseVerticalAxisName) * depthTranslation.sensitivity;
            transform.Translate(0, 0, translateZ);
        }
 
        if (scroll.isActivated())
        {
            float translateZ = Input.GetAxis(scrollAxisName) * scroll.sensitivity;
 
            transform.Translate(0, 0, translateZ);
        }*/
		
		//Follow mouse vertical and horizontal
		float translateY = Input.GetAxis(mouseVerticalAxisName) * verticalTranslation.sensitivity;
		float translateX = Input.GetAxis(mouseHorizontalAxisName) * horizontalTranslation.sensitivity;
		float translateZ = Input.GetAxis(mouseVerticalAxisName) * depthTranslation.sensitivity;
		//float X = transform.position.x;
		//float Y = transform.position.y;
		//if((X >= -5) && (X <= 5) && (Y >= -5) && (Y <= 5))
		//{
		transform.Translate(new Vector3(0, translateY, 0));	
		transform.Translate(new Vector3(translateX, 0, 0));
		transform.Translate(new Vector3(0, 0, translateZ));
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, (TopMonitor2.transform.renderer.bounds.min.x + 0.05f), (TopMonitor1.transform.renderer.bounds.max.x - 0.05f)), Mathf.Clamp(transform.position.y, (BotMonitor2.transform.renderer.bounds.min.y + 0.01f), (TopMonitor2.transform.renderer.bounds.max.y - 0.09f)),Mathf.Clamp(transform.position.z, BotMonitor2.transform.renderer.bounds.min.z, TopMonitor2.transform.renderer.bounds.max.z));

//******************************************************************************************************************************************************************************************************//		
//TOP

			if((transform.position.x >= TopMonitor1.transform.renderer.bounds.min.x) && (transform.position.x <= TopMonitor1.transform.renderer.bounds.max.x))
			{
				//IN TOP MONITOR 1
				transform.position = new Vector3(transform.position.x, transform.position.y, TopMonitor1.transform.renderer.bounds.max.z);
			}

			if((transform.position.x >= TopMonitor2.transform.renderer.bounds.max.x) && (transform.position.x <= TopMonitor1.transform.renderer.bounds.min.x)) 
			{
				//FROM TOP MONITOR 2 TO TOP MONITOR 1 OR TOP MONITOR  1 TO TOP MONITOR 2
				if (translateX < 0)
				{
					transform.position = new Vector3 (TopMonitor2.transform.renderer.bounds.max.x, transform.position.y, transform.position.z);
				//	transform.eulerAngles = new Vector3(0, 0, 0);
				}
				if (translateX > 0)
				{
					transform.position = new Vector3 (TopMonitor1.transform.renderer.bounds.min.x, transform.position.y, transform.position.z);
					//transform.eulerAngles = new Vector3(0, -20, 0);
				}
			}
		
			if((transform.position.x >= TopMonitor2.transform.renderer.bounds.min.x) && (transform.position.x <= TopMonitor2.transform.renderer.bounds.max.x))
			{
				//IN TOP MONITOR 2
				transform.position = new Vector3(transform.position.x, transform.position.y, TopMonitor2.transform.renderer.bounds.max.z);
			}
//*************************************************************************************************************************************************************************************************************
		//TRANSITION
			if((transform.position.y >= BotMonitor1.transform.renderer.bounds.max.y) && (transform.position.y <= TopMonitor1.transform.renderer.bounds.min.y))	
			{
			//FROM TOP MONITOR 1 TO BOTTOM MONITOR 1
				if (translateY < 0)
				{
					transform.position = new Vector3(transform.position.x, (BotMonitor1.transform.renderer.bounds.max.y - 0.075f), transform.position.z);
				}
			//FROM BOTTOM MONITOR 1 TO TOP MONITOR 1
				if (translateY > 0)
				{
					transform.position = new Vector3(transform.position.x, (TopMonitor1.transform.renderer.bounds.min.y + 0.01f), transform.position.z);
				}
			}
		
			if((transform.position.y >= BotMonitor2.transform.renderer.bounds.max.y) && (transform.position.y <= TopMonitor2.transform.renderer.bounds.min.y))	
			{
			//FROM TOP MONITOR 2 TO BOTTOM MONITOR 2
				if (translateY < 0)
				{
					transform.position = new Vector3(transform.position.x, (BotMonitor2.transform.renderer.bounds.max.y - 0.075f), transform.position.z);
				}
			//FROM BOTTOM MONITOR 2 TO TOP MONITOR 2
				if (translateY > 0)
				{
					transform.position = new Vector3(transform.position.x, (TopMonitor2.transform.renderer.bounds.min.y + 0.01f), transform.position.z);
				}
			}
//******************************************************************************************************************************************************************************************************//		
		//BOTTOM
		
				if((transform.position.x >= BotMonitor1.transform.renderer.bounds.min.x) && (transform.position.x <= BotMonitor1.transform.renderer.bounds.max.x))
		{
			//IN BOTTOM MONITOR 1
			transform.position = new Vector3(transform.position.x, transform.position.y, BotMonitor1.transform.renderer.bounds.max.z);
		}

		if((transform.position.x >= BotMonitor2.transform.renderer.bounds.max.x) && (transform.position.x <= BotMonitor1.transform.renderer.bounds.min.x)) 
		{
			//FROM BOTTOM MONITOR 2 TO BOTTOM MONITOR 1 OR BOTTOM MONITOR  1 TO BOTTOM MONITOR 2
			if (translateX < 0)
			{
				transform.position = new Vector3 (BotMonitor2.transform.renderer.bounds.max.x, transform.position.y, transform.position.z);
			//	transform.eulerAngles = new Vector3(0, 0, 0);
			}
			if (translateX > 0)
			{
				transform.position = new Vector3 (BotMonitor1.transform.renderer.bounds.min.x, transform.position.y, transform.position.z);
				//transform.eulerAngles = new Vector3(0, -20, 0);
			}
		}
		
		if((transform.position.x >= BotMonitor2.transform.renderer.bounds.min.x) && (transform.position.x <= BotMonitor2.transform.renderer.bounds.max.x))
		{
			//IN TOP MONITOR 2

			transform.position = new Vector3(transform.position.x, transform.position.y, BotMonitor2.transform.renderer.bounds.max.z);
		}
//******************************************************************************************************************************************************************************************************//				
	}
	
}
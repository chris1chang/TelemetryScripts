﻿using UnityEngine;
using System.Collections;

public class MouseControlP5_bottom : MonoBehaviour {
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
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, (Monitor3.transform.renderer.bounds.min.x + 0.05f), (Monitor1.transform.renderer.bounds.max.x - 0.05f)), Mathf.Clamp(transform.position.y, Monitor2.transform.renderer.bounds.min.y + 0.01f, Monitor2.transform.renderer.bounds.max.y - 0.09f),Mathf.Clamp(transform.position.z, Monitor2.transform.renderer.bounds.max.z, Monitor3.transform.renderer.bounds.max.z));
		//print ("Test 1");
		//}
		//	print("Y: " + translateY + "\tX: " + translateX + "\nX position: " + transform.position.x + " Y position: " + transform.position.y);
		//print ("Test 2");
		//print ("X:" + X + " Y:" + Y);

		if((transform.position.x >= Monitor1.transform.renderer.bounds.min.x) && (transform.position.x <= Monitor1.transform.renderer.bounds.max.x))
		{
			//IN MONITOR 1
			float thickness = Monitor2.transform.renderer.bounds.max.z - Monitor2.transform.renderer.bounds.min.z;
			float M1_z = Monitor1.transform.renderer.bounds.max.z - Monitor1.transform.renderer.bounds.min.z - thickness;
			float M1_x = Monitor1.transform.renderer.bounds.max.x - Monitor1.transform.renderer.bounds.min.x;
			float slope = M1_z/M1_x;
			//print ("z: " + M4_z + " x: " + M4_x + "slope: " + slope + "thickness: " + thickness);
			transform.position = new Vector3(transform.position.x, transform.position.y, ((transform.position.x)*slope - 4.0247043f));
		}

		if((transform.position.x >= Monitor2.transform.renderer.bounds.max.x) && (transform.position.x <= Monitor1.transform.renderer.bounds.min.x)) 
		{
			//FROM MONITOR 2 TO MONITOR 1 OR MONITOR  1 TO MONITOR 2
			if (translateX < 0)
			{
				transform.position = new Vector3 (Monitor2.transform.renderer.bounds.max.x, transform.position.y, transform.position.z);
			//	transform.eulerAngles = new Vector3(0, 0, 0);
			}
			if (translateX > 0)
			{
				transform.position = new Vector3 (Monitor1.transform.renderer.bounds.min.x, transform.position.y, transform.position.z);
				//transform.eulerAngles = new Vector3(0, -20, 0);
			}
		}
		
		if((transform.position.x >= Monitor2.transform.renderer.bounds.min.x) && (transform.position.x <= Monitor2.transform.renderer.bounds.max.x))
		{
			//MONITOR 2

			transform.position = new Vector3(transform.position.x, transform.position.y, Monitor2.transform.renderer.bounds.max.z);

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
			//	print ("1111111111111111111111111");
			float thickness = Monitor2.transform.renderer.bounds.max.z - Monitor2.transform.renderer.bounds.min.z;
			float M3_z = Monitor3.transform.renderer.bounds.max.z - Monitor3.transform.renderer.bounds.min.z - thickness;
			float M3_x = Monitor3.transform.renderer.bounds.max.x - Monitor3.transform.renderer.bounds.min.x;
			float slope = M3_z/M3_x;
			print ("y angle: " + Monitor3.transform.eulerAngles.y);
			print ("sin: " + Mathf.Sin(Monitor3.transform.eulerAngles.y * Mathf.Deg2Rad));
			//print ("z: " + M4_z + " x: " + M4_x + "slope: " + slope + "thickness: " + thickness);
			transform.position = new Vector3(transform.position.x, transform.position.y, -((transform.position.x)*slope) - 2.5863239f);
			//transform.eulerAngles = new Vector3(0, 20, 0);
			//	transform.Translate(new Vector3(0, translateY, 0));	
			//	transform.Translate(new Vector3(translateX, 0, 0));
			//	transform.Translate(new Vector3(0, 0, translateZ));
			//transform.position = new Vector3(Mathf.Clamp(transform.position.x,Monitor3.transform.renderer.bounds.min.x, Monitor2.transform.renderer.bounds.max.x), Mathf.Clamp(transform.position.y, Monitor3.transform.renderer.bounds.min.y, Monitor2.transform.renderer.bounds.max.y),Monitor2.transform.renderer.bounds.max.z);
			//	Color c = renderer.material.color;
			//	c.a = 1;
			//transform.renderer.material.color.a = 1;
			//t
		}
	}
	
}
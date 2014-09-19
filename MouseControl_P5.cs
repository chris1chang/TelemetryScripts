//
//Filename: MouseCameraControl.cs
//

using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse")]
public class MouseControl_P5 : MonoBehaviour
{
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
		var TopMonitor3 = GameObject.Find("Top Monitor 3");
		var TopMonitor4 = GameObject.Find("Top Monitor 4");
		var TopMonitor5 = GameObject.Find("Top Monitor 5");
		var BotMonitor1 = GameObject.Find("Bottom Monitor 1");
		var BotMonitor2 = GameObject.Find("Bottom Monitor 2");
		var BotMonitor3 = GameObject.Find("Bottom Monitor 3");
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
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, (TopMonitor5.transform.renderer.bounds.min.x + 0.05f), (TopMonitor1.transform.renderer.bounds.max.x - 0.05f)), Mathf.Clamp(transform.position.y, (TopMonitor2.transform.renderer.bounds.min.y + 0.01f), (TopMonitor2.transform.renderer.bounds.max.y - 0.09f)),Mathf.Clamp(transform.position.z, TopMonitor2.transform.renderer.bounds.min.z, TopMonitor4.transform.renderer.bounds.max.z));
		//print ("Test 1");
		//}
		//	print("Y: " + translateY + "\tX: " + translateX + "\nX position: " + transform.position.x + " Y position: " + transform.position.y);
		//print ("Test 2");
		//print ("X:" + X + " Y:" + Y);

		if((transform.position.x >= TopMonitor1.transform.renderer.bounds.min.x) && (transform.position.x <= TopMonitor1.transform.renderer.bounds.max.x))
		{
			//IN MONITOR 1
			float thickness = TopMonitor2.transform.renderer.bounds.max.z - TopMonitor2.transform.renderer.bounds.min.z;
			float M1_z = TopMonitor1.transform.renderer.bounds.max.z - TopMonitor1.transform.renderer.bounds.min.z - thickness;
			float M1_x = TopMonitor1.transform.renderer.bounds.max.x - TopMonitor1.transform.renderer.bounds.min.x;
			float slope = M1_z/M1_x;
			//print ("z: " + M4_z + " x: " + M4_x + "slope: " + slope + "thickness: " + thickness);
			transform.position = new Vector3(transform.position.x, transform.position.y, ((transform.position.x)*slope) - 4.45f);
			//transform.eulerAngles = new Vector3(0, 20, 0);
			//	transform.Translate(new Vector3(0, translateY, 0));	
			//	transform.Translate(new Vector3(translateX, 0, 0));
			//	transform.Translate(new Vector3(0, 0, translateZ));
			//transform.position = new Vector3(Mathf.Clamp(transform.position.x,TopMonitor3.transform.renderer.bounds.min.x, TopMonitor2.transform.renderer.bounds.max.x), Mathf.Clamp(transform.position.y, TopMonitor3.transform.renderer.bounds.min.y, TopMonitor2.transform.renderer.bounds.max.y), TopMonitor2.transform.renderer.bounds.max.z);
			//	Color c = renderer.material.color;
			//	c.a = 1;
			//transform.renderer.material.color.a = 1;
			//transform.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		}

		if((transform.position.x >= TopMonitor2.transform.renderer.bounds.max.x) && (transform.position.x <= TopMonitor1.transform.renderer.bounds.min.x)) 
		{
			//FROM MONITOR 2 TO MONITOR 1 OR MONITOR  1 TO MONITOR 2
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
			//MONITOR 2

			transform.position = new Vector3(transform.position.x, transform.position.y, TopMonitor2.transform.renderer.bounds.max.z);
			//	transform.Translate(new Vector3(0, translateY, 0));	
			//	transform.Translate(new Vector3(translateX, 0, 0));
			//	transform.Translate(new Vector3(0, 0, translateZ));
			//transform.position = new Vector3(Mathf.Clamp(transform.position.x, TopMonitor3.transform.renderer.bounds.min.x, TopMonitor2.transform.renderer.bounds.max.x), Mathf.Clamp(transform.position.y, TopMonitor3.transform.renderer.bounds.min.y, TopMonitor2.transform.renderer.bounds.max.y),TopMonitor2.transform.renderer.bounds.max.z);
			//	Color c = renderer.material.color;
			//	c.a = 1;
			//transform.renderer.material.color.a = 1;
			//transform.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		}

		if((transform.position.x >= TopMonitor3.transform.renderer.bounds.max.x) && (transform.position.x <= TopMonitor2.transform.renderer.bounds.min.x)) 
		{
			//FROM MONITOR 2 TO MONITOR 3 OR MONITOR  3 TO MONITOR 2
			if (translateX < 0)
			{
				transform.position = new Vector3 (TopMonitor3.transform.renderer.bounds.max.x, transform.position.y, transform.position.z);
			}
			if (translateX > 0)
			{
				transform.position = new Vector3 (TopMonitor2.transform.renderer.bounds.min.x, transform.position.y, transform.position.z);
			}
		}

		if((transform.position.x >= TopMonitor3.transform.renderer.bounds.min.x) && (transform.position.x <= TopMonitor3.transform.renderer.bounds.max.x))
		{
			//IN MONITOR 3
			transform.position = new Vector3(transform.position.x, transform.position.y, TopMonitor3.transform.renderer.bounds.max.z);
			//	transform.Translate(new Vector3(0, translateY, 0));	
			//	transform.Translate(new Vector3(translateX, 0, 0));
			//	transform.Translate(new Vector3(0, 0, translateZ));
			//transform.position = new Vector3(Mathf.Clamp(transform.position.x,TopMonitor3.transform.renderer.bounds.min.x, TopMonitor2.transform.renderer.bounds.max.x), Mathf.Clamp(transform.position.y, Monitor3.transform.renderer.bounds.min.y, Monitor2.transform.renderer.bounds.max.y),Monitor2.transform.renderer.bounds.max.z);
			//	Color c = renderer.material.color;
			//	c.a = 1;
			//transform.renderer.material.color.a = 1;
			//transform.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		}

		if((transform.position.x <= TopMonitor3.transform.renderer.bounds.min.x) && (transform.position.x >= TopMonitor4.transform.renderer.bounds.max.x)) 
		{
			//FROM 3 TO 4 OR 4 TO 3
			if (translateX < 0)
			{
				transform.position = new Vector3 (TopMonitor4.transform.renderer.bounds.max.x, transform.position.y, Mathf.Clamp(transform.position.z, transform.position.z, TopMonitor4.transform.renderer.bounds.max.z));
				//	transform.eulerAngles = new Vector3(0, 20, 0);
				//transform.position = new Vector3 (transform.position.x, transform.position.y,Mathf.Clamp (transform.position.z, TopMonitor4.transform.renderer.bounds.min.z, TopMonitor4.transform.renderer.bounds.max.z));
				//	print ("yes");
			}
			if (translateX > 0)
			{
				transform.position = new Vector3 (TopMonitor3.transform.renderer.bounds.min.x, transform.position.y, transform.position.z);
				//	print ("no");		
				//	transform.eulerAngles = new Vector3(0, 0, 0);
			}
		}
		
		if((transform.position.x >= TopMonitor4.transform.renderer.bounds.min.x) && (transform.position.x <= TopMonitor4.transform.renderer.bounds.max.x))
		{
			//IN MONITOR 4
			transform.position = new Vector3(transform.position.x, transform.position.y, TopMonitor4.transform.renderer.bounds.max.z);
			//	transform.Translate(new Vector3(0, translateY, 0));	
			//	transform.Translate(new Vector3(translateX, 0, 0));
			//	transform.Translate(new Vector3(0, 0, translateZ));
			//transform.position = new Vector3(Mathf.Clamp(transform.position.x,TopMonitor3.transform.renderer.bounds.min.x, TopMonitor2.transform.renderer.bounds.max.x), Mathf.Clamp(transform.position.y, TopMonitor3.transform.renderer.bounds.min.y, TopMonitor2.transform.renderer.bounds.max.y), TopMonitor2.transform.renderer.bounds.max.z);
			//	Color c = renderer.material.color;
			//	c.a = 1;
			//transform.renderer.material.color.a = 1;
			//transform.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		}
		
			if((transform.position.x <= TopMonitor4.transform.renderer.bounds.min.x) && (transform.position.x >= TopMonitor5.transform.renderer.bounds.max.x)) 
		{
			//FROM 4 TO 5 OR 5 TO 4
			if (translateX < 0)
			{
				transform.position = new Vector3 (TopMonitor5.transform.renderer.bounds.max.x, transform.position.y, Mathf.Clamp(transform.position.z, transform.position.z, TopMonitor5.transform.renderer.bounds.max.z));
				//	transform.eulerAngles = new Vector3(0, 20, 0);
				//transform.position = new Vector3 (transform.position.x, transform.position.y,Mathf.Clamp (transform.position.z, TopMonitor4.transform.renderer.bounds.min.z, TopMonitor4.transform.renderer.bounds.max.z));
				//	print ("yes");
			}
			if (translateX > 0)
			{
				transform.position = new Vector3 (TopMonitor4.transform.renderer.bounds.min.x, transform.position.y, transform.position.z);
				//	print ("no");		
				//	transform.eulerAngles = new Vector3(0, 0, 0);
			}
		}
		

		if((transform.position.x >= TopMonitor5.transform.renderer.bounds.min.x) && (transform.position.x <= TopMonitor5.transform.renderer.bounds.max.x))
		{
			//IN MONITOR 5
			//	print ("1111111111111111111111111");
			float thickness = TopMonitor2.transform.renderer.bounds.max.z - TopMonitor2.transform.renderer.bounds.min.z;
			float M5_z = TopMonitor5.transform.renderer.bounds.max.z - TopMonitor5.transform.renderer.bounds.min.z - thickness;
			float M5_x = TopMonitor5.transform.renderer.bounds.max.x - TopMonitor5.transform.renderer.bounds.min.x;
			float slope = M5_z/M5_x;
			print ("y angle: " + TopMonitor5.transform.eulerAngles.y);
			print ("sin: " + Mathf.Sin(TopMonitor5.transform.eulerAngles.y * Mathf.Deg2Rad));
			//print ("z: " + M5_z + " x: " + M5_x + "slope: " + slope + "thickness: " + thickness);
			transform.position = new Vector3(transform.position.x, transform.position.y, (-(transform.position.x)*slope) - 3.0522879f);//-((transform.position.x)*slope) - 2.7555529f);
			//transform.eulerAngles = new Vector3(0, 20, 0);
			//	transform.Translate(new Vector3(0, translateY, 0));	
			//	transform.Translate(new Vector3(translateX, 0, 0));
			//	transform.Translate(new Vector3(0, 0, translateZ));
			//transform.position = new Vector3(Mathf.Clamp(transform.position.x,Monitor3.transform.renderer.bounds.min.x, Monitor2.transform.renderer.bounds.max.x), Mathf.Clamp(transform.position.y, Monitor3.transform.renderer.bounds.min.y, Monitor2.transform.renderer.bounds.max.y),Monitor2.transform.renderer.bounds.max.z);
			//	Color c = renderer.material.color;
			//	c.a = 1;
			//transform.renderer.material.color.a = 1;
			//transform.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		}
	/*	if ((transform.position.x >= Monitor1.transform.renderer.bounds.min.x) && (transform.position.x <= Monitor1.transform.renderer.bounds.max.x))
		{
			print ("333333333333333333333333333333");
		//	transform.position = new Vector3(Mathf.Clamp(transform.position.x,Monitor1.transform.renderer.bounds.min.x, Monitor1.transform.renderer.bounds.max.x), Mathf.Clamp(transform.position.y, Monitor1.transform.renderer.bounds.min.y, Monitor1.transform.renderer.bounds.max.y),Monitor1.transform.renderer.bounds.max.z);
		//	Color c = renderer.material.color;
		//	c.a = 1;
			//transform.renderer.material.color.a = 1;
			//transform.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		}*/
	//	if ((transform.position.x >= Monitor4.transform.renderer.bounds.min.x) && (transform.position.x <= Monitor4.transform.renderer.bounds.max.x))
	//	{
	//		print ("444444444444444444444444444444444");
			//transform.position = new Vector3(Mathf.Clamp(transform.position.x,transform.position.x, Monitor4.transform.renderer.bounds.max.x), Mathf.Clamp(transform.position.y, Monitor4.transform.renderer.bounds.min.y, Monitor4.transform.renderer.bounds.max.y),Monitor4.transform.renderer.bounds.max.z);
		//	Color c = renderer.material.color;
		//	c.a = 1;
			//transform.renderer.material.color.a = 1;
			//transform.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
	//	}
		//else
		//{
		//	Color c = renderer.material.color;
		//	c.a = 0.1f;
			//transform.renderer.material.color.a = 0.000000000001;
			//transform.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		//}
	/*		print ("Moniter 1 min = " + Monitor1.transform.renderer.bounds.min.x);
		print ("Moniter 1 max = " + Monitor1.transform.renderer.bounds.max.x);
		print ("Moniter 2 min = " + Monitor2.transform.renderer.bounds.min.x);
		print ("Moniter 2 max = " + Monitor2.transform.renderer.bounds.max.x);
		print ("Moniter 3 min = " + Monitor3.transform.renderer.bounds.min.x);
		print ("Moniter 3 max = " + Monitor3.transform.renderer.bounds.max.x);
		print ("Moniter 4 min = " + Monitor4.transform.renderer.bounds.min.x);
		print ("Moniter 4 max = " + Monitor4.transform.renderer.bounds.max.x);*/
		//if (transform.position = new Vector3(
		//transform.position.y = Mathf.Clamp(transform.position.y, -5, 5);
		//transform.position = new Vector3(0, Mathf.Clamp(transform.position.y, -5, 5), 0);
	//print ("mouse: " + translateX +"," + translateY + "," + translateZ);
	//	print ("mouse 2: " + transform.position.x);
	//	print ("M3: " + Monitor3.transform.renderer.bounds.min.x + " M4: " + Monitor4.transform.renderer.bounds.max.x);
	}
	
}
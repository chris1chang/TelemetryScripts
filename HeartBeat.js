//This file updates the heartbeat and oxygen values. Also calls upon the ecg signals
#pragma strict
import System;
import System.IO;

//public variables
public var colV : Color = new Color(0f,0f,0f);
public var mon : int = 0;
public var num : int  = 0;
public var graphType : int = 1;
public var graphPanel : GameObject;
public var systemText : GameObject;
public var patientText : GameObject;
public var redBox : GameObject;
public var oxygenText : GameObject;
public var oxygenLabel : GameObject;
public var background : GameObject;
public var alarm : int = 1;
var myClip : AudioClip;

//private variables
private var grapher : LineGrapher;
private var sr : StreamReader;
private var line : String;
private var text;
private var CubeColl : CubeColision;
private var flag : int = 0;

function Start(){
	//Read in the correct file for the patient
	if(Application.loadedLevelName == "Telemetry_P2_Low_Alarm_Rate"){
		text = Application.dataPath+"/Resources/P2/Patient"+mon+"-"+num+"-LH.txt";
		}
	else if(Application.loadedLevelName == "Telemetry_P2_High_Alarm_Rate"){
		text = Application.dataPath+"/Resources/P2H/Patient"+mon+"-"+num+"-HH.txt";
		}
	else{
		text = Application.dataPath+"/Resources/P1/Patient"+mon+"-"+num+"-H.txt";
		}		
	sr = File.OpenText(text);
    line = "";
    if(!graphType){
    	//set redbox off, set text to Discharged, blank out heartbeat text
    	redBox.renderer.enabled = false; 
    	systemText.GetComponent(TextMesh).color = Color.red;
    	setSystemText("");
    	GetComponent(TextMesh).text = "";
    	oxygenText.GetComponent(TextMesh).text = "";
    	patientText.GetComponent(TextMesh).text = "";
    }//NoPatient Case
    while(graphType){
    	redBox.renderer.enabled = true;
    	//systemText.GetComponent(TextMesh).color = Color.white;
    	updateHB();
    	yield WaitForSeconds(1.0);//Heartbeat delay - updates once per second
    }//Ends loop that runs forever
    setVolume(true);
}//Ends start method

//Custom update method
public function updateHB(){
	if(line != null){
		line = sr.ReadLine();
	    if (line != null){ 
	    	//public var Volume : float = 1.0;
	    	var lineArray : String[] = line.Split(","[0]);
	    	var beat = int.Parse(lineArray[0]);
	    	var oxy = int.Parse(lineArray[1]);
	    	var tex = GetComponent(TextMesh);
	    	tex.color = colV;
	    	tex.text = ((beat < 100) ? " "+beat : lineArray[0]);
	    	oxygenText.GetComponent(TextMesh).text = ((oxy < 100) ? "% "+oxy: lineArray[1]);
	    	setOxygenLabel(" ");
	    	if(beat > 100){
	    		oxygenText.GetComponent(TextMesh).text = ""; 
	    		setAlarm(1);//yellow
	    		audio.PlayOneShot(myClip);//alarm
	    	}else if(beat < 60){
	    		oxygenText.GetComponent(TextMesh).text = "";
	    		setAlarm(2);//yellow
	    		audio.PlayOneShot(myClip);//alarm
	    		Debug.Log("volume " + audio.volume);
	    	}else if(oxy < 90){
	    		setAlarm(3);//red
	    		setOxygenLabel("SPO2:");
	    		audio.PlayOneShot(myClip);//alarm
	    		Debug.Log("volume " + audio.volume);
	    	}//if (oxy >= 90){
	    	//	oxygenText.GetComponent(TextMesh).text = ""; 
	    	//	setOxygenLabel(" ");
	    	//}
	    	else{
	    		oxygenText.GetComponent(TextMesh).text = ""; 
	    		setOxygenLabel(" ");
	    		setAlarm(0);//white
	    		setVolume(true);
	   	}
	   //	Debug.Log("mon " + mon +"num " + num);
	   //	Debug.Log(beat);
	   	}else{
	    	sr.Close(); 
	    	GetComponent(TextMesh).text = "0"; 
	    }//End check
	}//Ends check for valid file
}//Ends function

//Function that sets the color of the graph panel border
function setAlarm(value : int){
	switch(value){
		case 0: //Normal Patient Case
		if (flag == 1){
			for (var child : Transform in graphPanel.transform)
				child.renderer.material.color = Color.white; //Set border color
			for (var child : Transform in graphPanel.transform)
				child.renderer.material.color.a = 0.25; //Set border color	
		} 
		background.renderer.material.color = Color.black;
		flag = 0;
			setSystemText(" "); //Set System Text
			setOxygenLabel(" ");
			break;
		case 1: //Tachycardia Patient Case
			for (var child : Transform in graphPanel.transform)
				if(Application.loadedLevelName == "TelemetryP4_10patients"){
				child.renderer.material.color = Color.red; //Set border color
				}
				else if (Application.loadedLevelName == "Telemetry_P7"){
				blink();
				}
				else if(Application.loadedLevelName == "TelemetryP8_Green"){
				child.renderer.material.color = Color.green; //Set border color
				}
				else if(Application.loadedLevelName == "TelemetryP8_Blue"){
				child.renderer.material.color = Color.blue; //Set border color
				}
				else if(Application.loadedLevelName == "TelemetryP8_Yellow"){
				child.renderer.material.color = Color.yellow; //Set border color
				}
				else{
				child.renderer.material.color = Color.yellow; //Set border color
				}
			setSystemText("Tachycardia Alert");//Set System Text
			flag = 1;
			break;
		case 2: //Bradycardia Patient Case
			for (var child : Transform in graphPanel.transform)
			if(Application.loadedLevel == "TelemetryP8_Green"){
				child.renderer.material.color = Color.green; //Set border color
				}
			else if (Application.loadedLevelName == "Telemetry_P7"){
				blink();
				}	
			else if(Application.loadedLevelName == "TelemetryP8_Yellow"){
				child.renderer.material.color = Color.yellow; //Set border color
				}
			else if(Application.loadedLevelName == "TelemetryP8_Blue"){
				child.renderer.material.color = Color.blue; //Set border color
				}
			else{
				child.renderer.material.color = Color.red; //Set border color
				}
			setSystemText("Bradycardia Alert"); //Set System Text
			flag = 1;
			break;
		case 3: //Hypoximia Patient Case
			for (var child : Transform in graphPanel.transform)
			if(Application.loadedLevelName == "TelemetryP8_Green"){
				child.renderer.material.color = Color.green; //Set border color
				}
			else if (Application.loadedLevelName == "Telemetry_P7"){
				blink();
				}	
			else if(Application.loadedLevelName == "TelemetryP8_Yellow"){
				child.renderer.material.color = Color.yellow; //Set border color
				}
			else if(Application.loadedLevelName == "TelemetryP8_Blue"){
				child.renderer.material.color = Color.blue; //Set border color
				}
			else{
				child.renderer.material.color = Color.red; //Set border color
				}
			setSystemText("Hypoxemia Alert"); //Set System Text
			flag = 1;
			break;
		default: //Default case
			for (var child : Transform in graphPanel.transform)
				child.renderer.material.color = Color.white; 
			setSystemText(" ");
			setOxygenLabel(" ");
			break;
	}//Ends switch cases
}//Ends function

	
//function to change the System text of a patient
function setSystemText(value : String){
	systemText.GetComponent(TextMesh).text = value;
}//Ends function

function setOxygenLabel(value : String){
	oxygenLabel.GetComponent(TextMesh).text = value;
}

function blink(){ 
	var seconds : int = Time.time;
	var oddeven = (seconds % 2);
		if(oddeven == 1){
		background.renderer.material.color = Color.red;
		systemText.renderer.material.color = Color.white;
		}
		else if (oddeven == 0){
		background.renderer.material.color = Color.black;
		systemText.renderer.material.color = Color.white;
		}
		systemText.renderer.material.color = Color.red;
		flag = 1;
}

function setVolume(value : boolean)
{
	if(true){
	AudioListener.volume = 1.0;
	}else{
	AudioListener.volume = 0.2;	
	Debug.Log("volume " + value);    	
	}
}

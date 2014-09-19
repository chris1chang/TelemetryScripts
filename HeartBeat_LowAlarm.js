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
var myClip : AudioClip = UnityEngine.Resources.Load("Beep");

//private variables
private var grapher : LineGrapher;
private var sr : StreamReader;
private var line : String;


function Start(){
Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

	//Read in the correct file for the patient
	var text = Application.dataPath+"/Resources/Patient"+mon+"-"+num+"-LH.txt";
	sr = File.OpenText(text);
    line = "";
    if(!graphType){
    	//set redbox off, set text to Discharged, blank out heartbeat text
    	redBox.renderer.enabled = false; 
    	systemText.GetComponent(TextMesh).color = Color.red;
    	setSystemText("Discharged");
    	GetComponent(TextMesh).text = "";
    	oxygenText.GetComponent(TextMesh).text = "";
    	patientText.GetComponent(TextMesh).text = "";
    }//NoPatient Case
    while(graphType){
    	redBox.renderer.enabled = true;
    	systemText.GetComponent(TextMesh).color = Color.white;
    	updateHB();
    	yield WaitForSeconds(1.0);//Heartbeat delay - updates once per second
    }//Ends loop that runs forever
}//Ends start method

//Custom update method
function updateHB(){
	if(line != null){
		line = sr.ReadLine();
	    if (line != null){ 
	    	var lineArray : String[] = line.Split(","[0]);
	    	var beat = int.Parse(lineArray[0]);
	    	var oxy = int.Parse(lineArray[1]);
	    	var tex = GetComponent(TextMesh);
	    	tex.color = colV;
	    	tex.text = ((beat < 100) ? " "+beat : lineArray[0]);
	    	oxygenText.GetComponent(TextMesh).text = ((oxy < 100) ? " "+oxy: lineArray[1]); 
	    	if(beat > 100){
	    		audio.PlayOneShot(myClip);//alarm
	    		setAlarm(1);//yellow
	    	}else if(beat < 60){
	    		setAlarm(2);//yellow
	    		audio.PlayOneShot(myClip);
	    	}else if(oxy < 90){
	    		setAlarm(3);//red
	    		audio.PlayOneShot(myClip);
	    	}else
	    		setAlarm(0);//white
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
		for (var child : Transform in graphPanel.transform)
				child.renderer.material.color = Color.white; //Set border color
			setSystemText("Patient Stable"); //Set System Text
			break;
		case 1: //Tachycardia Patient Case
			for (var child : Transform in graphPanel.transform)
				child.renderer.material.color = Color.yellow; //Set border color
			setSystemText("Tachycardia Alert");//Set System Text
			break;
		case 2: //Bradycardia Patient Case
			for (var child : Transform in graphPanel.transform)
				child.renderer.material.color = Color.red; //Set border color
			setSystemText("Bradycardia Alert"); //Set System Text
			break;
		case 3: //Hypoximia Patient Case
			for (var child : Transform in graphPanel.transform)
				child.renderer.material.color = Color.red; //Set border color
			setSystemText("Hypoximia Alert"); //Set System Text
			break;
		default: //Default case
			for (var child : Transform in graphPanel.transform)
				child.renderer.material.color = Color.white; 
			setSystemText("Patient Stable");
			break;
	}//Ends switch cases
}//Ends function

	
//function to change the System text of a patient
function setSystemText(value : String){
	systemText.GetComponent(TextMesh).text = value;
}//Ends function


//Monitor Scfript handles resizing windows and displaying graphs on Bottom panel
#pragma strict

//Public Variables
public var panelPrefab : GameObject;		//Prefab for border panel
public var patientPrefab : GameObject;		//Prefab for patient panel
public var upperLeftLocation : GameObject;	//Passed reference of upper left position of monitor
public var bottomRightLocation : GameObject;//Passed reference of bottom right position of monitor
public var numOfPatients = 10;				//Default number of patients per monitor
public var highlightFlag = 0;				//Public variable to flag if monitor has a selected patient(or not)
public var monitorNum = 0;					//Monitor index number
//Private variables
private var length : float = 0;				//calculated length of monitor
private var height : float = 0;				//calculated height of monitor
private var midPoint : float = 0;			//calculated midpoint of monitor
private var depth : float = 0;				//temp var: calculated depth of monitor (z difference)
private var panX : float = 0.4809006;		//length scaling factor for patient panel 
private var panY : float = 0;				//height scaling factor for patient panle
private var bottomPanel : GameObject;		//reference to botom panel with ecg signals

//Init method to create monitor with a certain number of patient panels
function Start () {
	sizeInit();
	patientPanelsInit();
	bottomPanelInit();
}//Ends start method

//TODO: Optimization: read in a big MonitorDataFile to graphs get array of all patient graphs
function Update () {
}//Ends function

//Function that determines length & width of screen
function sizeInit(){
	length = upperLeftLocation.transform.position.x - bottomRightLocation.transform.position.x;
	height = upperLeftLocation.transform.position.y - bottomRightLocation.transform.position.y;
	depth = upperLeftLocation.transform.position.z - bottomRightLocation.transform.position.z;
	midPoint = (upperLeftLocation.transform.position.z + bottomRightLocation.transform.position.z)/2.0;//Constant
	panY = height*2.0f/(numOfPatients);//Constant
}//Ends functionn

//Initialization method for creating the number of patient panels
function patientPanelsInit(){
	for(var i = 1; i <= numOfPatients; i++){
		var panelAct : panelActions;
		var patientPanel = Instantiate(patientPrefab, transform.position, transform.rotation);
		//Make it a child of the monitpr
		patientPanel.transform.parent = transform;
		//Set position, scaling factors, color and make it invisible for now
		patientPanel.transform.position = upperLeftLocation.transform.position;
		patientPanel.transform.rotation = upperLeftLocation.transform.rotation;
		patientPanel.transform.localScale.x = panX;
		patientPanel.transform.localScale.y = panY*1.1f;
		patientPanel.transform.position.y -= panY/2.0f + panY*(i-1-Mathf.RoundToInt(i/2));//Constant
		patientPanel.transform.position.x -= length/4.0f; //Constant
		patientPanel.transform.position.z -= depth/4.0f; //Constant
		if(i%2 == 0){//Patient is on the right column
			patientPanel.transform.position.x -= length/2.0f; //Constant
			patientPanel.transform.position.z -= depth/2.0f; //Constant
		}//Set the patient panels on the right side of the screen
		panelAct = patientPanel.GetComponent(panelActions);
		panelAct.number = i;
		panelAct.mon = monitorNum;
		panelAct.setColor(Random.Range(0,6));
		panelAct.setHeight(panY);
		//Pass location of monitor
		panelAct.upLeft = upperLeftLocation;
		panelAct.boRight = bottomRightLocation;
	}//Ends instantiating all panels on monitor
}//Ends function

//TODO:fix resecaling and spacing for non-constant values(below is hardcoded)
//Initialization method for creating the botom data panel
function bottomPanelInit(){
	//Create new bottom Panel
	bottomPanel = Instantiate(panelPrefab, transform.position, transform.rotation);
	//Make it a child of the monitpr
	bottomPanel.transform.parent = transform;
	//Set position, scaling factors, color and make it invisible for now
	bottomPanel.transform.position = bottomRightLocation.transform.position;
	bottomPanel.transform.rotation = bottomRightLocation.transform.rotation;
	bottomPanel.transform.position.x += length/2.0f; //Constant
	bottomPanel.transform.position.y += height/4.0f; //Constant
	bottomPanel.transform.position.z = midPoint+.01f;
	bottomPanel.transform.localScale.x = 0.9825;//TODO - find constant
	bottomPanel.transform.localScale.y = 0.4;	//TODO - find constant
	bottomPanel.renderer.material.color = Color.black;
	bottomPanel.renderer.material.color.a = 1.0f;	//Constant
	bottomPanel.renderer.enabled = false;
	for (var child : Transform in bottomPanel.transform) {
		child.renderer.enabled = false;
	}//Turns on all borders of the bottom panel
}//Ends function
	
//Toggle method to turn on/off the bottom monitor panel
function monitorPanel(value : boolean){
	if(value){
		bottomPanel.renderer.enabled = true;
		for (var child : Transform in bottomPanel.transform) {
			child.renderer.enabled = true;
		}//Turns on panel parts
	}else{
		bottomPanel.renderer.enabled = false;
		for (var child : Transform in bottomPanel.transform) {
			child.renderer.enabled = false;
		}//Turns off panel parts
	}//Ends check
}//Ends function

//Function call to toggle resizing patient panels 
//to make room for bottom data panel
function resizeAllPanels(value : boolean){
	var panelActs : Component[] = GetComponentsInChildren(panelActions);
	if(value){
		for (var child : panelActions in panelActs) {
			child.resizeAction(true);
			//Move panel depending on original location
			switch(child.number%4){
				case 2:
					child.shift(-1);break;
				case 3:
					child.up(1);child.shift(2);break;
				case 0:
					child.up(1);child.shift(1);break;
			}//Ends switch
			child.up(Mathf.Floor((child.number-1)/4));
		}//Ends resizing panels to %50 of original size
	}else{
		for (var child : panelActions in panelActs) {
			child.resizeAction(false);
			//Move panel back depending on where original location was
			switch(child.number%4){
				case 2:
					child.shift(1);break;
				case 3:
					child.up(-1);child.shift(-2);break;
				case 0:
					child.up(-1);child.shift(-1);break;
			}//Ends switch
			child.up(-Mathf.Floor((child.number-1)/4));
		}//Ends resizing panels to original size
	}//Ends check
}//Ends function

//Function turns off all highlights
function turnOffAllPanels(){
	var panelActs : Component[] = GetComponentsInChildren(panelActions);
	for (var child : panelActions in panelActs){
		child.setHighlight(false);
	}//Ends loop to turn off all highlight flags
}//Ends function
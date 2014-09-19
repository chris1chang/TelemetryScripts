//PanelActions sets the panel with a highlighted border and scales to %50
#pragma strict

//Patient number
public var number : int = 0;		//Patient index number
public var mon : int = 0;			//Monitor index number
public var highlightFlag = 0;		//higlight true/false flag
public var upLeft : GameObject;		//Passed reference of upper left position of monitor
public var boRight : GameObject;	//Passed reference of bottom right position of monitor
//Private vars
private var scale = 0.505;			//Scaling factor
private var color = 0;				//Internal color index value
private var dataGraph : GameObject;	//Reference to ECG graph gameobject of patient
private var graphPanel : GameObject;//Reference to outer graph border/panel of patient panel
private var graphText : GameObject;	//Reference to immediate text surrouding the graph 
private var innerPanel : GameObject;//Reference to inner box containing heartbeat value
private var innerText : GameObject;	//Reference to text inside the inner box
private var heartRate : GameObject;	//Reference to heartbeate value
private var grapher : LineGrapher;	//reference to script/actions of ecg graph
private var lowalarmheart : HeartBeat_LowAlarm;
private var height = 0f;			//height of panel
private var colV : Color;			//color vector

//Init method
function Start(){
Debug.Log("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
	getColor();
	for (var child : Transform in transform) {
		if (child.name == "dataGraph") 
			dataGraph = child.gameObject;
		if (child.name == "graphPanel"){
			graphPanel = child.gameObject;
			grapher = dataGraph.GetComponent(LineGrapher);
			grapher.colV = colV;
			grapher.num = number;
			grapher.mon = mon;
			
			//Either have all panels On, or randomize some to be off( not both)
			//grapher.graphType = ((Random.Range(0,16) < 5) ? 0:1);//Randomizes which panels should be off
			grapher.graphType = 1; //Sets all panels to be on
			
			grapher.highlightFlag = 0;
			grapher.upL = upLeft;
			grapher.boR = boRight;
			grapher.transform.position.y += 0.45f*height; //Set graph to 40% of panel height
			//grapher.transform.position.x +=  0.01f;
		}
		if (child.name == "graphText"){
			graphText = child.gameObject;
			var texts : Component[];
			texts = GetComponentsInChildren(TextMesh);
			for (var tex : TextMesh in texts) {
				if(tex.name == "text_patientNumber"){
					var X:int = (mon-1)*16+(number-1);
					var rem:int = X%5;
					var R:int = (X-rem)/5 +1;
					var P:int = X%5 +1;
					tex.text = P+"P"+R+"R1F";//((number<10)?"0":"")+number;
				}//Set Patient location, ex: 1P1R1F
			}//Ends loop
		}//Ends check
		if (child.name == "heartRate"){
			heartRate = child.gameObject;
			lowalarmheart = heartRate.GetComponent(HeartBeat_LowAlarm);
			lowalarmheart.num = number;
			lowalarmheart.mon = mon;
			lowalarmheart.colV = colV;
			lowalarmheart.graphType = grapher.graphType;
		}//Ends check
		if (child.name == "innerPanel")
			innerPanel = child.gameObject;
		if (child.name == "innerText") 
			innerText = child.gameObject;
	}//Ends loop
}//Ends start method

//Function to set the color of the graph
function setColor(value : int){
	color = value;
	getColor();
}//Ends setColor method

//Function to set the height of the graph
function setHeight(value : float){
	height = value;
}//Ends function

//Returns the color of the graph
function getColor(){
	switch(color){
		case 0: //White
			colV = new Color(1f, 1f, 1f); break;
		case 1: //Red
			colV = new Color(.85f, 0f, 0f); break;
		case 2: //Green
			colV = new Color(0f, .85f, 0f); break;
		case 3: //Blue
			colV = new Color(0f, .65f, .85f); break;
		case 4: //Purple
			colV = new Color(.7f, .2f, 2f); break;
		case 5: //Yellow
			colV = new Color(.85f, .85f, 0f); break;
		case 6: //Pink
			colV = new Color(1f, 0f, .8f); break;
		default://White 
			colV = new Color(1f, 1f, 1f); break;
	}//Ends switch
}//ends method getColor

//TODO: make the border of the selected patient "highlight" by increasing alpha values
//Toggle function to highlight patient panel border
function setHighlight(value : boolean){
	Debug.Log("PA:highlight is "+value);
	if(value){
		highlightFlag = 1;
		grapher.highlightFlag = 1;
		//for (var child in graphPanel.GetComponentsInChildren(Renderer)) {
		//	child.renderer.material.color.a = 0.50;
		//	child.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		//}//loop throguh parts of graph panel
	}else{	
		highlightFlag = 0;
		grapher.highlightFlag = 0;	
		//for (var child in graphPanel.GetComponentsInChildren(Renderer)) {
		//	child.renderer.material.color.a = 1.0f;
		//	child.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		//}//loop throguh parts of graph panel
	}//ends check
}//Ends highlight function

//Toggle function to resize an individual patient panel
function resizeAction(value : boolean){
	if(value){
		graphPanel.transform.localScale.x *= scale;
		graphPanel.transform.localPosition.x += scale/2.0f;
		innerPanel.transform.localPosition.x += scale;
		innerText.transform.localPosition.x += scale;
		heartRate.transform.localPosition.x += scale;
		graphText.transform.localPosition.x += scale/2.0f;
		dataGraph.transform.localPosition.x += scale;
		grapher.Min();
	}else{
		graphPanel.transform.localScale.x /= scale;
		graphPanel.transform.localPosition.x -= scale/2.0f;
		innerPanel.transform.localPosition.x -= scale;
		innerText.transform.localPosition.x -= scale;
		heartRate.transform.localPosition.x -= scale;
		graphText.transform.localPosition.x -= scale/2.0f;
		dataGraph.transform.localPosition.x -= scale;
		grapher.Max();
	}//Ends check
}//Ends funtion

//Function shifts the patient panel:
//-to the right with a positive number, left with a negative
//-factor is a scaling factor
function shift(factor : float){ 
	graphPanel.transform.localPosition.x -= factor*scale;
	innerPanel.transform.localPosition.x -= factor*scale;
	innerText.transform.localPosition.x -= factor*scale;
	heartRate.transform.localPosition.x -= factor*scale;
	graphText.transform.localPosition.x -= factor*scale;
	dataGraph.transform.localPosition.x -= factor*scale;
}//Ends shift function

//Function that moves the patient panel up
function up(factor : float){
	factor *= 2.01f;
	graphPanel.transform.localPosition.y += factor*scale;
	innerPanel.transform.localPosition.y += factor*scale;
	innerText.transform.localPosition.y += factor*scale;
	heartRate.transform.localPosition.y += factor*scale;
	graphText.transform.localPosition.y += factor*scale;
	dataGraph.transform.localPosition.y += factor*scale;
}//ends up function


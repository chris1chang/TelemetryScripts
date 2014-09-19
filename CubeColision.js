//HandCollision.js handles telling the correct patient panel to highlight,
#pragma strict

//Action scripts for the monitor and panels
private var panelAct : panelActions;		//reference to script/actions of patient panel
private var monitorAct : monitorActions;	//reference to script/actions of monitor
private var GraphPanelAct : GraphPanelActions;

//Main interaction trigger method
function OnTriggerStay(col : Collider){
	if(col.gameObject.name == "patientPanel" || col.gameObject.name == "patientPanel(Clone)" || col.gameObject.name == "patientPanel 1" || col.gameObject.name == "patientPanel 1(Clone)"){
		//Setup actions prefab for Panel actions
		panelAct = col.GetComponent(panelActions); 
		//Setup actions prefab for monitor actions
		monitorAct = col.transform.parent.GetComponent(monitorActions);
		if((monitorAct.highlightFlag == 0) && Input.GetMouseButton(0)){
		    //Highlight patient
				panelAct.setHighlight(true);
				//GraphPanelAct.Bold(true);
				//panelAct.resizeAction(true);
			//Resize and reposition all panels on monitor
				//monitorAct.resizeAllPanels(true);
			//Create new bottom panel
				//monitorAct.monitorPanel(true);
			//Set flag to "on"
			monitorAct.highlightFlag = 1;
		}else if((monitorAct.highlightFlag == 1) && Input.GetMouseButton(0)){
			//if current patient panel was previously selected/highlighted
			if(panelAct.highlightFlag == 1){
				//Set highlight off
					panelAct.setHighlight(false);
				//GraphPanelAct.Bold(false);
					//panelAct.resizeAction(false);
				//Remove bottom panel
					//monitorAct.monitorPanel(false);
				//Resize patient panel to original size 
					//monitorAct.resizeAllPanels(false);
				//Reset flag to "off"
					monitorAct.highlightFlag = 0;
			}else if(panelAct.highlightFlag == 0){
				//Set all other panels' highlight off
					monitorAct.turnOffAllPanels();
				//Highlight patient
					panelAct.setHighlight(true);
					//GraphPanelAct.Bold(true);
					//panelAct.resizeAction(true);
			}//Ends check if selected new patient
		}//Ends check if a patient is already selected
	}//ends check if collision was with a patient box
}//Ends collision method call	
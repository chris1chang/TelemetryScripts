#pragma strict
private var TBorder : GameObject;
private var LBorder : GameObject;
private var RBorder : GameObject;
private var BBorder : GameObject;

function Start () {
	for (var child : Transform in transform) {
			if( child.name == "Bottom_border")
			BBorder = child.gameObject;
			if( child.name == "Left_border")
			LBorder = child.gameObject;
			if( child.name == "Right_border")
			RBorder = child.gameObject;
			if( child.name == "Top_border")
			TBorder = child.gameObject;
		}	
}

function Update () {

}

function Bold (value : boolean) {
	if(value){
		BBorder.transform.localScale += Vector3(0,0.1,0);
		TBorder.transform.localScale += Vector3(0,0.1,0);
		RBorder.transform.localScale += Vector3(0.1,0,0);
		LBorder.transform.localScale += Vector3(0.1,0,0);
	}
	else {
		BBorder.transform.localScale += Vector3(0,-0.1,0);
		TBorder.transform.localScale += Vector3(0,-0.1,0);
		RBorder.transform.localScale += Vector3(-0.1,0,0);
		LBorder.transform.localScale += Vector3(-0.1,0,0);
		}
}
	
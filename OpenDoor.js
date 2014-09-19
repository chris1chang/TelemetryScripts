var AngleX : float = 0.0;
var AngleY : float = 90.0;
var AngleZ : float = 0.0;

private var targetValue : float = 0.0;
private var currentValue : float = 0.0;
private var easing : float = 0.05;

var Target : GameObject;

function Update () {
	currentValue = currentValue + (targetValue - currentValue) * easing;
	
	Target.transform.rotation = Quaternion.identity;
	Target.transform.Rotate(0, currentValue, 0);
}

function OnTriggerEnter (other : Collider){
	targetValue = AngleY;
	currentValue = 0;
}

function OnTriggerExit (other : Collider){
	currentValue = AngleY;
	targetValue = 0.0;
}
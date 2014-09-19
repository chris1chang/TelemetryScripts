#pragma strict

private var enter : boolean;
private var transparent : boolean;
var counter = 1;

function Start () {
//transform.renderer.material.color.a = 0.01;
//transform.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );

}

function Update () {
print(transparent);
/*
	if(transparent == true) //not on screen
	{
		transform.renderer.material.color.a -= 0.1;
		transform.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		print("transparent");
	}
	if(transparent == false) //on screen
	{
		transform.renderer.material.color.a = 1;
		transform.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
		print("you messed up");
	}*/
}

function OnTriggerEnter(other : Collider){
if (other.gameObject.tag == "mouse"){ 
other.gameObject.transform.renderer.material.color.a = 1;
other.gameObject.transform.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
transparent = false;
//counter++;
}
}

function OnTriggerStay(other : Collider){
if (other.gameObject.tag == "mouse"){ 
other.gameObject.transform.transform.renderer.material.color.a = 1;
other.gameObject.transform.transform.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
transparent = false;
//counter++;
}
}

function OnTriggerExit (other : Collider){
if (other.gameObject.tag == "mouse"){ 
transform.renderer.material.color.a = 0.000000000001;
transform.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
transparent = true;
}
}
#pragma strict

function Start () {
//	gameObject.renderer.material.color = Color.red;
	gameObject.renderer.material.color.a = 0.5;
	gameObject.renderer.material.shader = Shader.Find( "Transparent/Diffuse" );
//	Debug.Log(gameObject.renderer.material.color.a);

}

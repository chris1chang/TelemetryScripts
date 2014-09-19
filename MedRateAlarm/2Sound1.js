#pragma strict
var sound : AudioClip;
 
function Start(){
    InvokeRepeating("PlayClipAndChange",Random.Range(0, 10.0),10.0f);
}
 
function PlayClipAndChange(){
    audio.Play();
}
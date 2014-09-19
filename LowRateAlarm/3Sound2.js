#pragma strict
var sound : AudioClip;
 
function Start(){
    InvokeRepeating("PlayClipAndChange",Random.Range(0, 15.0),15.0f);
}
 
function PlayClipAndChange(){
    audio.Play();
}
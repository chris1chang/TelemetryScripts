#pragma strict
var sound : AudioClip;
 
function Start(){
    InvokeRepeating("PlayClipAndChange",Random.Range(0, 5.0),5.0f);
}
 
function PlayClipAndChange(){
    audio.Play();
}
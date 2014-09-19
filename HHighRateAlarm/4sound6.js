#pragma strict
var sound : AudioClip;
 
function Start(){
    InvokeRepeating("PlayClipAndChange",Random.Range(0, 2.5f),2.5f);
}
 
function PlayClipAndChange(){
    audio.Play();
}
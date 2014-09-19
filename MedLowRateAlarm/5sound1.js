#pragma strict
var sound : AudioClip;
 
function Start(){
    InvokeRepeating("PlayClipAndChange",Random.Range(0, 7.5f),7.5f);
}
 
function PlayClipAndChange(){
    audio.Play();
}
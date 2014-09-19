var sound : AudioClip[] = new AudioClip[3];
public var lower_cap : int;
public var upper_cap : int;
 
function Start(){
    InvokeRepeating("PlayClipAndChange",0.01f,Random.Range(lower_cap, upper_cap));
}
 
function PlayClipAndChange(){
    audio.clip = sound[Random.Range(0, 3)];
    audio.Play();
    Debug.Log("SOUND");
}
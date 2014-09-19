#pragma strict

function Start () {

    var someNumber : float;
    someNumber = 2;
    animation.Play("Walk");
    animation["Walk"].speed = someNumber;

}

function Update () {

}
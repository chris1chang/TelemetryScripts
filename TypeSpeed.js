#pragma strict

function Start () {

    var someNumber : float;
    someNumber = 0.5;
    animation.Play("Type");
    animation["Type"].speed = someNumber;

}

function Update () {

}
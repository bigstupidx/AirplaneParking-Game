#pragma strict

 @script ExecuteInEditMode()
function OnEnable () {
    if(Application.isEditor&&!Application.isPlaying) //only run it if we are in edit mode.
        this.transform.position.z=585;
    else{
        this.transform.position.z=0;    
    }
}

function Start () {

}

function Update () {

}
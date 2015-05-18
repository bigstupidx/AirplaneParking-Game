#pragma strict

class CarTouchExternalInput extends CarExternalInput
{


    var steer = 0.0;
    var acceleration = 0.0;
    var brake = 0;


    function Awake() 
    {
        super();
    }


    function Update() {
        m_CarControl.steerInput = steer;
        m_CarControl.motorInput = acceleration;
        m_CarControl.brakeInput = brake;
        //m_CarControl.gearInput = m_targetGear;
        m_CarControl.gearInput = 1;

        m_CarControl.handbrakeInput = 0.0;
    }
}
using UnityEngine;
using System.Collections;

public class SharpInput : MonoBehaviour {
    private CarTouchExternalInput _input;
    private float _steer;
    private float _curSteer;
    private float _motor;
    private float _targetSteer;
    private int standBrake;
    // Use this for initialization
	void Start () {
        _input = GetComponent<CarTouchExternalInput>();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        float xVel = transform.InverseTransformDirection(rigidbody.velocity).z;


        _curSteer = Mathf.Lerp(_curSteer, _steer, Time.deltaTime * 5f);
        _input.steer = _curSteer;
        _input.brake = (_motor < 0 && xVel > 0.03f) || (_motor > 0 && xVel < -0.03f) ? 1 : 0;

	    if (_input.brake != 1 || standBrake == 1)
	    {
	        _input.acceleration = _motor;
	    }
	    else
	    {
	        _input.acceleration = 0;
	    }

        standBrake = (_motor == 0 && xVel < 0.3f && xVel > -0.3f) ? 1 : 0;;
	    if (standBrake == 1)
	    {
	        _input.brake = standBrake;
	    }
	}

    public void Steer(float v)
    {
        _steer = v;
    }

    public void Motor(float v)
    {
        _motor = v;

    }
}

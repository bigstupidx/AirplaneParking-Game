using UnityEngine;
using System.Collections;

public class SteerInput : MonoBehaviour 
{
    public SharpInput _sharpInput;
    private SteeringWheel _wheel;
	void Start ()
	{
        _wheel = GetComponent<SteeringWheel>();
	}
	void Update () 
    {
        _sharpInput.Steer(_wheel.GetClampedValue());
	}
}

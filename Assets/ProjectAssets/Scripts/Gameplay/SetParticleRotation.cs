using UnityEngine;
using System.Collections;

public class SetParticleRotation : MonoBehaviour {
    private ParticleSystem _particleSystem;
	// Use this for initialization
	void Start () 
    {
        _particleSystem = GetComponent<ParticleSystem>();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _particleSystem.startRotation = transform.eulerAngles.y*Mathf.Deg2Rad;
	}
}

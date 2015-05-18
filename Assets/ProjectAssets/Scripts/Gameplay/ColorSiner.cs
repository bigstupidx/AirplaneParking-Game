using UnityEngine;
using System.Collections;

public class ColorSiner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	    this.renderer.material.color = new Color(0,1, 0, Mathf.Abs(Mathf.Cos(Time.time*3)*2));

	}
}

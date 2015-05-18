using UnityEngine;
using System.Collections;

public class DestroyOnLoad : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start ()
	{
	    yield return null;
	    yield return null;
	    yield return null;
        Destroy(gameObject);


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

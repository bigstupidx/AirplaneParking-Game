using UnityEngine;
using System.Collections;

public class Activator : MonoBehaviour
{

    public GameObject go;
	// Use this for initialization
	void Awake () {

        go.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

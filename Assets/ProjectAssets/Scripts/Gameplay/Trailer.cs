using UnityEngine;
using System.Collections;

public class Trailer : MonoBehaviour
{
    public bool Parked { get; private set; }

    // Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	    //GetComponent<CarTouchExternalInput>();

	}

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Floor"))
        {
            Debug.Log("Collided with " + collision.gameObject.name);
            LevelControl.I.FailLevel();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        var parking = collider.GetComponent<ParkingCollider>();
        if (parking)
        {
            Parked = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        var parking = collider.GetComponent<ParkingCollider>();
        if (parking)
        {
            Parked = false;
        }
    }

}

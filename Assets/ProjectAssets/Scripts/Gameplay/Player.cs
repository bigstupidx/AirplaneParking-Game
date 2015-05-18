using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Trailer Trailer;
    public PointDirection Arrow;
    private bool _parked;
    private bool _finished;
	// Use this for initialization
	void Start ()
	{
	    I = this;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (_parked && Trailer.Parked)
	    {
	        if (rigidbody.velocity.magnitude < 1 && !_finished)
	        {
	            LevelControl.I.FinishLevel();
	            _finished = true;
	        }
	    }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Floor"))
        LevelControl.I.FailLevel();
    }

    void OnTriggerEnter(Collider collider)
    {
        var parking = collider.GetComponent<ParkingCollider>();
        if (parking)
        {
            //if (parking.FirstCollider)
            {
                _parked = true;
            }
        }

    }

    void OnTriggerExit(Collider collider)
    {
        var parking = collider.GetComponent<ParkingCollider>();
        if (parking)
        {
            //if (parking.FirstCollider)
            _parked = false;
        }
    }

    public static Player I { get; private set; }
}

using UnityEngine;
using System.Collections;

public class PointDirection : MonoBehaviour
{

    public Transform Target;

    private Quaternion _target;
    private bool ok = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
	    if (Target != null)
	    {
            _target = Quaternion.LookRotation(-transform.position + Target.position);

	        transform.rotation = Quaternion.Lerp(transform.rotation, _target, Time.deltaTime);

            if (Target.GetComponentInChildren<Renderer>().isVisible && false)
	        {
	            if (ok)
	            {
	                var r = gameObject.GetComponentsInChildren<Renderer>();
	                foreach (var re in r)
	                {
	                    re.enabled = false;
	                }
	                ok = false;
	            }
	        }
	        else
	        {
	            if (!ok)
	            {
	                var r = gameObject.GetComponentsInChildren<Renderer>();
	                foreach (var re in r)
	                {
	                    re.enabled = true;
	                }
	                ok = true;
	            }
	        }

	        //transform.LookAt(Target.position, Vector3.up);
	    }
	}
}

using UnityEngine;
using System.Collections;
using System.Linq;

public class UIRoot : MonoBehaviour {

	// Use this for initialization
	void Awake ()
	{
	    I = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public T GetView<T>() where T: View
    {
        return transform.GetComponentsInChildren<T>(true).FirstOrDefault();
    }

    public static UIRoot I { get; private set; }
}

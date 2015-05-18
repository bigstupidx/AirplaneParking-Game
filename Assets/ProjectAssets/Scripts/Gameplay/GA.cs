using UnityEngine;

public class GA : MonoBehaviour
{
    public static GoogleAnalyticsV3 I;

	void Start ()
	{
	    I = GetComponent<GoogleAnalyticsV3>();
	}
}

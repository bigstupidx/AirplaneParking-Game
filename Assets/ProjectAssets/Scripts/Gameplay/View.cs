using UnityEngine;
using System.Collections;

public class View : MonoBehaviour 
{

    public void SetVisible(bool value)
    {
        gameObject.SetActive(value);
    }
}

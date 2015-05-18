using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PreCloseScreenImageElement : MonoBehaviour 
{
    public Image GuiObject;

    [NonSerialized]
    public bool Placed;

    [NonSerialized]
    public string Url;
}

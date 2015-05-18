using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour 
{
    /// <summary>
    /// Which events will be called in event controller
    /// </summary>
    public List<string> OnClick;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            foreach (var @event in OnClick)
            {
                EventController.I.PostEvent(@event, gameObject);
            }
        });
    }
}

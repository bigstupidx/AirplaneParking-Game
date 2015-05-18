using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public interface IEventController
{
    void PostEvent(string EventName, GameObject Sender = null);
    void Subscribe(string name, IEventSubscriber subscriber);
    void Unsubscribe(string eventName, IEventSubscriber subscriber);
}

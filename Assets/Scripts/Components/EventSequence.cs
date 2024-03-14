using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventSequence : MonoBehaviour
{
    public List<UnityEvent> events;

    public void ExecuteEvent()
    {
        if (events.Count > 0)
        {
            if (events[0] != null)
                events[0].Invoke();
            events.RemoveAt(0);
        }
    }
}

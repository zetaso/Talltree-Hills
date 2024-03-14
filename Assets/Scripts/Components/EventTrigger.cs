using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public UnityEvent on_trigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interact interact = other.GetComponent<Interact>();
        if (interact && on_trigger != null)
        {
            on_trigger.Invoke();
        }
    }
}

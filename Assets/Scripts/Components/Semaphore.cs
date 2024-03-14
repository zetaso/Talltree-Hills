using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Semaphore : MonoBehaviour
{
    public int goal, current;

    public UnityEvent onReachGoal;

    public void Signal()
    {
        current++;

        if (current == goal && onReachGoal != null)
            onReachGoal.Invoke();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public bool is_complete { get; protected set; }
    float start_time;
    protected float time => Time.time - start_time;

    public virtual void Trigger() { }

    public virtual void Enter()
    {
        start_time = Time.time;
    }

    public virtual void Do() { }

    public virtual State Next() { return null; }

    public virtual void Exit() { is_complete = false; }

    public virtual void Setup(MonoBehaviour provider) { }
}

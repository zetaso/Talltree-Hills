using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlyAggro : State
{
    protected Flying flying;
    public UnityEvent onAggro;

    public override void Trigger()
    {
        flying.SetNextState(ref flying.mode, this);
    }

    public override void Enter()
    {
        base.Enter();
        flying.fly_anticipate.Trigger();
        if (onAggro != null)
        {
            onAggro.Invoke();
            onAggro.RemoveAllListeners();
            onAggro = null;
        }
    }

    public override void Do() { }

    public override State Next()
    {
        return null;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        flying = provider as Flying;
    }
}

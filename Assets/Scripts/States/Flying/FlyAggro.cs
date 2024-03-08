using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAggro : State
{
    protected Flying flying;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        flying.fly_anticipate.Trigger();
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

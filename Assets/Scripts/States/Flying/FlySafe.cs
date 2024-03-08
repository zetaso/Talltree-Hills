using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySafe : State
{
    protected Flying flying;

    public float min_time_to_aggro, max_time_to_aggro;
    float time_to_aggro;

    public override void Trigger()
    {
        flying.SetNextState(ref flying.mode, this);
    }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;
        time_to_aggro = Random.Range(min_time_to_aggro, max_time_to_aggro);
    }

    public override void Do()
    {

        if (time >= time_to_aggro)
            is_complete = true;
    }

    public override State Next()
    {
        if (flying.health.health > 0)
            return flying.fly_aggro;
        return null;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        flying = provider as Flying;
    }
}

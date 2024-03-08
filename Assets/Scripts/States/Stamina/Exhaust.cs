using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhaust : State
{
    Stamina provider;

    public float exhaust_time;

    public override void Enter()
    {
        base.Enter();
        is_complete = false;
    }

    public override void Do()
    {
        if (time >= exhaust_time)
            is_complete = true;
    }

    public override State Next()
    {
        return provider.recover;
    }

    public override void Exit()
    {
        is_complete = false;
    }

    public override void Setup(MonoBehaviour provider)
    {
        this.provider = provider as Stamina;
    }
}

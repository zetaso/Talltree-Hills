using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : State
{
    Stamina provider;
    public float stamina_duration;

    public override void Enter()
    {
        is_complete = false;
    }

    public override void Trigger()
    {
        provider.SetNextState(this);
    }

    public override void Do()
    {
        provider.stamina = Mathf.Max(0, provider.stamina - Time.deltaTime / stamina_duration);
        if (provider.stamina == 0)
            is_complete = true;
    }

    public override State Next()
    {
        return provider.exhaust;
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

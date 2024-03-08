using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recover : State
{
    Stamina provider;

    public float recover_time, recovering_alpha;

    public override void Enter()
    {
        is_complete = false;
        provider.alpha = recovering_alpha;
    }

    public override void Do()
    {
        provider.stamina = Mathf.Min(1, provider.stamina + Time.deltaTime / recover_time);
        if (provider.stamina == 1)
            is_complete = true;
    }

    public override State Next()
    {
        return provider.full;
    }

    public override void Exit()
    {
        is_complete = false;
        provider.alpha = 1;
    }

    public override void Setup(MonoBehaviour provider)
    {
        this.provider = provider as Stamina;
    }
}

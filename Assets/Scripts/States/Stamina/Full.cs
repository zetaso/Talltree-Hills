using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Full : State
{
    Stamina provider;

    public override void Enter()
    {
        is_complete = true;
    }

    public override State Next()
    {
        return null;
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

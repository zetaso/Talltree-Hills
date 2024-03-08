using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inactive : Open
{
    public float alpha;

    public void ForceExit()
    {
        is_complete = true;
    }

    public override void Trigger()
    {
        crosshair.SetNextState(this);
    }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;
        crosshair.SetAlpha(alpha);
    }

    public override State Next()
    {
        return crosshair.open;
    }

    public override void Exit()
    {
        base.Exit();
        crosshair.SetAlpha(1);
    }
}

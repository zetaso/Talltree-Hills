using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Improve : State
{
    [SerializeField] Action action;
    Crosshair crosshair;

    public float improve_time;

    public override void Enter()
    {
        is_complete = true;
    }

    public override void Do()
    {
        crosshair.accuracy = Mathf.Min(1, crosshair.accuracy + Time.deltaTime / improve_time);
    }

    public override State Next()
    {
        if (action.state == action.hold)
            return crosshair.open;
        return null;
    }

    public override void Exit()
    {
    }

    public override void Setup(MonoBehaviour provider)
    {
        crosshair = provider as Crosshair;
    }
}

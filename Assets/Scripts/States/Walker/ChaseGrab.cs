using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseGrab : State
{
    Chase chase;

    public string upper_clip_name, lower_clip_name;
    public float grab_range;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        // make player character still and die animation
    }

    public override void Do() { }

    public override State Next() { return null; }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        chase = provider as Chase;
    }
}

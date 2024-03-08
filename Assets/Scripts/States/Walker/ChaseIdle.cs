using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseIdle : State
{
    Chase chase;

    public string upper_clip_name, lower_clip_name;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = true;

        chase.walker.upper_animator.Play(upper_clip_name);
        chase.walker.lower_animator.Play(lower_clip_name);

        chase.time_since_idle = 0;
    }

    public override void Do()
    {
    }

    public override State Next()
    {
        //raycast between walker and player logic
        if (Vector2.Distance(chase.walker.transform.position, chase.walker.target.position) <= chase.vision_range)
            return chase.run;

        return null;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        chase = provider as Chase;
    }
}

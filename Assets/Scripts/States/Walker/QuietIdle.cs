using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuietIdle : State
{
    Quiet quiet;

    public string upper_clip_name, lower_clip_name;
    public float time_idle;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        quiet.walker.upper_animator.Play(upper_clip_name);
        quiet.walker.lower_animator.Play(lower_clip_name);
    }

    public override void Do()
    {
        if (time >= time_idle)
            is_complete = true;
    }

    public override State Next()
    {
        return quiet.walk;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        quiet = provider as Quiet;
    }
}

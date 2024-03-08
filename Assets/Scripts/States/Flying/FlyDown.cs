using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDown : State
{
    protected Fly fly;
    public string clip_name;

    public float time_to_exit;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        fly.flying.animator.Play(clip_name);
        is_complete = false;
    }

    public override void Do()
    {
        if (time >= time_to_exit && fly.visuals.localPosition.y < fly.max_height)
            is_complete = true;
    }

    public override State Next()
    {
        return fly.fly_up;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        fly = provider as Fly;
    }
}


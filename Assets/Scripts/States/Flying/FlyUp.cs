using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyUp : State
{
    protected Fly fly;
    public string clip_name;

    public float jump_velocity, y_velocity_to_exit;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        fly.flying.animator.Play(clip_name);
        fly.y_velocity = jump_velocity;
        is_complete = false;
    }

    public override void Do()
    {
        if (fly.y_velocity <= y_velocity_to_exit)
            is_complete = true;
    }

    public override State Next()
    {
        return fly.fly_down;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        fly = provider as Fly;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCatch : State
{
    protected Flying flying;
    public string clip_name;

    public float time_in_ground;
    public Collider2D ground_collider;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        flying.animator.Play(clip_name);
        ground_collider.enabled = true;
    }

    public override void Do()
    {
        if (time >= time_in_ground)
            is_complete = true;
    }

    public override State Next()
    {
        return flying.fly;
    }

    public override void Exit()
    {
        is_complete = false;
        Destroy(flying.fly_anticipate.target_indicator);
        ground_collider.enabled = false;
    }

    public override void Setup(MonoBehaviour provider)
    {
        flying = provider as Flying;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCatch : State
{
    Chase chase;

    public string lower_clip_name;
    public float catch_range;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        chase.walker.lower_animator.Play(lower_clip_name);
        chase.walker.upper_renderer.enabled = false;
        // make player character still and die animation
    }

    public override void Do()
    {
        chase.walker.rb.velocity = Vector2.zero;
    }

    public override State Next() { return null; }

    public override void Exit()
    {
        is_complete = false;
        chase.walker.upper_renderer.enabled = false;
    }

    public override void Setup(MonoBehaviour provider)
    {
        chase = provider as Chase;
    }
}

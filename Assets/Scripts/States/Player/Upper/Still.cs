using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Still : State
{
    Action action;
    public string clip_name;

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        action.animator.Play(clip_name);
        action.movement.rb.velocity = Vector2.zero;
        action.movement.ren.enabled = false;
    }

    public override State Next()
    {
        return null;
    }

    public override void Exit()
    {
        is_complete = false;

        action.movement.ren.enabled = true;
    }

    public override void Setup(MonoBehaviour provider)
    {
        action = provider as Action;
    }
}

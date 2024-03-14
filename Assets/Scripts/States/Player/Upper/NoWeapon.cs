using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoWeapon : State
{
    Action action;
    public string clip_name;

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        action.animator.Play(clip_name);
    }

    public override void Do()
    {
    }

    public override State Next()
    {
        return action.hold;
    }

    public override void Exit()
    {
        is_complete = false;
    }

    public override void Setup(MonoBehaviour provider)
    {
        action = provider as Action;
    }
}

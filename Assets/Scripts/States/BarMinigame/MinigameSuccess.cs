using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameSuccess : State
{
    BarMinigame minigame;

    public Escape escape;

    public override void Enter()
    {
        base.Enter();
        is_complete = true;

        escape.action.SetNextState(escape.action.hold);
        minigame.escape.action.animator.speed = 1f;
    }

    public override void Do() { }

    public override State Next()
    {
        return minigame.vanish;
    }

    public override void Exit()
    {
        is_complete = false;
    }

    public override void Setup(MonoBehaviour provider)
    {
        minigame = provider as BarMinigame;
    }
}
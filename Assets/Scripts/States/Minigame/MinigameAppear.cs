using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameAppear : State
{
    BarMinigame minigame;

    float start_time;
    protected float utime => Time.unscaledTime - start_time;
    public float appear_time;

    public override void Enter()
    {
        base.Enter();
        start_time = Time.unscaledTime;
        is_complete = false;

        minigame.attemps++;
        minigame.escape.action.animator.speed = 0.5f + 0.5f * minigame.attemps;

        minigame.fade.enabled = true;
        minigame.fade.time = appear_time;
        minigame.fade.start_alpha = 0;
        minigame.fade.end_alpha = 1;
        minigame.fade.Restart();
    }

    public override void Do()
    {
        if (utime >= appear_time)
            is_complete = true;
    }

    public override State Next()
    {
        return minigame.move;
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

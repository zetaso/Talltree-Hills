using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameAppear : State
{
    BarMinigame minigame;

    float utime_passed;
    public float time_to_appear;
    public float appear_time;

    bool appearing;

    public override void Enter()
    {
        base.Enter();
        utime_passed = 0;
        is_complete = false;

        minigame.attemps = 0;
        appearing = false;
    }

    public override void Do()
    {
        utime_passed += Utils.unpausedDeltaTime;

        if (!appearing && utime_passed >= time_to_appear)
        {
            minigame.fade.enabled = true;
            minigame.fade.time = appear_time;
            minigame.fade.start_alpha = 0;
            minigame.fade.end_alpha = 1;
            minigame.fade.Restart();
            appearing = true;
        }

        if (utime_passed >= time_to_appear + appear_time)
            is_complete = true;
    }

    public override State Next()
    {
        return minigame.reposition;
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

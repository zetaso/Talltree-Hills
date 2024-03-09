using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameVanish : State
{
    BarMinigame minigame;

    float start_time;
    protected float utime => Time.unscaledTime - start_time;
    public float vanish_time;

    public override void Enter()
    {
        base.Enter();
        start_time = Time.unscaledTime;
        is_complete = false;

        minigame.fade.enabled = true;
        minigame.fade.time = vanish_time;
        minigame.fade.start_alpha = 1;
        minigame.fade.end_alpha = 0;
        minigame.fade.Restart();

        minigame.cursor_and_area_fade.enabled = true;
        minigame.cursor_and_area_fade.time = vanish_time;
        minigame.cursor_and_area_fade.start_alpha = 1;
        minigame.cursor_and_area_fade.end_alpha = 0;
        minigame.cursor_and_area_fade.Restart();
    }

    public override void Do()
    {
        if (utime >= vanish_time)
        {
            
        }
    }

    public override State Next()
    {
        return null;
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

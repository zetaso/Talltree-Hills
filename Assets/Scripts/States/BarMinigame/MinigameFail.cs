using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameFail : State
{
    BarMinigame minigame;

    float utime_passed;
    public float vanish_time, speed, desaccel;

    public override void Enter()
    {
        base.Enter();
        utime_passed = 0;
        is_complete = false;

        minigame.cursor_and_area_fade.enabled = true;
        minigame.cursor_and_area_fade.time = vanish_time;
        minigame.cursor_and_area_fade.start_alpha = 1;
        minigame.cursor_and_area_fade.end_alpha = 0;
        minigame.cursor_and_area_fade.Restart();
    }

    public override void Do()
    {
        utime_passed += Utils.unpausedDeltaTime;

        speed = Mathf.Max(0, speed - desaccel * Time.unscaledDeltaTime);
        minigame.cursor.transform.position += Vector3.right * speed * Time.unscaledDeltaTime;

        if (utime_passed >= vanish_time)
            is_complete = true;
    }

    public override State Next()
    {
        if (minigame.attemps < 3)
            return minigame.reposition;
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
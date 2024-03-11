using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameReposition : State
{
    BarMinigame minigame;

    float utime_passed;
    public float appear_time, wait_time;

    public override void Enter()
    {
        base.Enter();
        utime_passed = 0;
        is_complete = false;

        minigame.attemps++;
        minigame.escape.action.animator.speed = 1 + 0.5f * (minigame.attemps - 1);

        float area_width = 1 - 0.25f * minigame.attemps;
        minigame.area.size = new Vector2(area_width, 0.75f);

        float bar_width = minigame.bar.size.x - 0.5f - area_width;  //width padded for the area rect
        minigame.area.transform.localPosition = Vector3.right * bar_width * 0.5f + Vector3.left * Random.Range(0, 0.667f) * bar_width;

        bar_width = minigame.bar.size.x - 0.5f - 0.125f;  //width padded for the cursor rect
        float x_min = -bar_width * 0.5f;
        float x_max = minigame.area.transform.localPosition.x - area_width * 0.5f - 0.125f - 0.5f;
        float x = x_max >= x_min ? Random.Range(x_min, x_min + (x_max - x_min) * 0.5f) : x_min;
        minigame.cursor.transform.localPosition = Vector3.right * x;

        minigame.cursor_and_area_fade.enabled = true;
        minigame.cursor_and_area_fade.time = appear_time;
        minigame.cursor_and_area_fade.start_alpha = 0;
        minigame.cursor_and_area_fade.end_alpha = 1;
        minigame.cursor_and_area_fade.Restart();
    }

    public override void Do()
    {

        utime_passed += Utils.unpausedDeltaTime;

        if (utime_passed >= appear_time + wait_time)
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

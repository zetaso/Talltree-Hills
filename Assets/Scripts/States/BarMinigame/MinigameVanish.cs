using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameVanish : State
{
    BarMinigame minigame;

    float utime_passed;
    public float vanish_time;

    public override void Enter()
    {
        base.Enter();
        utime_passed = 0;
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

        Utils.Instance.player_health.SetDamaging(false);
    }

    public override void Do()
    {
        utime_passed += Utils.unpausedDeltaTime;

        if (utime_passed >= vanish_time)
            is_complete = true;

        minigame.transform.localPosition = Vector3.down;
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

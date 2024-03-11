using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysMinigameVanish : State
{
    KeysMinigame minigame;
    public float vanish_time;

    public FightSpider fight_spider;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        minigame.fade.enabled = true;
        minigame.fade.time = vanish_time;
        minigame.fade.start_alpha = 1;
        minigame.fade.end_alpha = 0;
        minigame.fade.Restart();

        fight_spider.is_complete = true;
    }

    public override void Do()
    {
        if (time >= vanish_time)
            is_complete = true;
    }

    public override State Next()
    {
        return null;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        minigame = provider as KeysMinigame;
    }
}

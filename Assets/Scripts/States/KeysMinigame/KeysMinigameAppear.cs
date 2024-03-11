using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysMinigameAppear : State
{
    KeysMinigame minigame;
    public float appear_time;
    public Animator a_key, d_key;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        minigame.fade.enabled = true;
        minigame.fade.time = appear_time;
        minigame.fade.start_alpha = 0;
        minigame.fade.end_alpha = 1;
        minigame.fade.Restart();

        a_key.Play("a_type");
        d_key.Play("d_type");
    }

    public override void Do()
    {
        if (time >= appear_time)
            is_complete = true;
    }

    public override State Next()
    {
        return minigame.play;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        minigame = provider as KeysMinigame;
    }
}

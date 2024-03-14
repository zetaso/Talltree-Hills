using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSpider : State
{
    public Action action;
    public string clip_name;
    public KeysMinigame minigame;

    public Spider catcher;

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        minigame.SetNextState(minigame.appear);

        action.animator.Play(clip_name);
        action.animator.transform.localPosition = Vector3.zero;
        action.movement.animator.GetComponent<SpriteRenderer>().enabled = false;

        float angle = 270;
        action.direction.SetDirection(angle / 360f);
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
        catcher.range.hug.is_complete = true;
        catcher = null;
        action.movement.animator.GetComponent<SpriteRenderer>().enabled = true;

        if (minigame.state != minigame.vanish)
            minigame.SetNextState(minigame.vanish);
    }

    public override void Setup(MonoBehaviour provider)
    {
        action = provider as Action;
    }
}

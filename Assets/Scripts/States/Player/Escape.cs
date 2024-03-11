using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : State
{
    public Action action { get; private set; }

    public string clip_name;

    public BarMinigame bar_minigame;
    public FlyCatch catcher;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        action.animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        action.animator.Play(clip_name);
        action.movement.animator.GetComponent<SpriteRenderer>().enabled = false;

        bar_minigame.SetNextState(bar_minigame.appear);

        action.movement.rb.velocity = Vector2.zero;
        Time.timeScale = 0;
    }

    public override void Do() { }

    public override State Next() { return null; }

    public override void Exit()
    {
        is_complete = false;
        action.movement.animator.GetComponent<SpriteRenderer>().enabled = true;
        Time.timeScale = 1;
        action.animator.updateMode = AnimatorUpdateMode.Normal;
    }

    public override void Setup(MonoBehaviour provider)
    {
        action = provider as Action;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCatch : State
{
    public Flying flying { get; private set; }
    public string clip_name;

    public Action action;
    public float time_in_ground, catch_range;
    public Collider2D ground_collider;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        flying.animator.Play(clip_name);
        ground_collider.enabled = true;

        if (Vector2.Scale(flying.player.position - flying.transform.position, new Vector2(1, 2)).magnitude <= catch_range)
        {
            Action action = flying.player.GetComponent<Action>();
            action.SetNextState(action.escape);
            action.escape.catcher = this;
            flying.transform.position = action.transform.position;
        }
    }

    public override void Do()
    {
        if (time >= time_in_ground && action.state != action.escape)
            is_complete = true;
    }

    public override State Next()
    {
        return flying.fly;
    }

    public override void Exit()
    {
        is_complete = false;
        Destroy(flying.fly_anticipate.target_indicator);
        ground_collider.enabled = false;
    }

    public override void Setup(MonoBehaviour provider)
    {
        flying = provider as Flying;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFall : State
{
    protected Flying flying;
    public string clip_name;

    public float fall_speed;

    public override void Trigger()
    {
        flying.SetNextState(ref flying.state, this);
    }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        flying.animator.Play(clip_name);

        flying.fade.enabled = true;
        flying.fade.start_alpha = flying.fade.current_alpha;
        flying.fade.end_alpha = 1;
        flying.fade.time = 0.25f;
        flying.fade.Restart();
    }

    public override void Do()
    {
        flying.fly.visuals.localPosition = Vector3.up * Mathf.Max(0, flying.fly.visuals.localPosition.y - fall_speed * Time.deltaTime);
        flying.shadow.color = new Color(1, 1, 1, Mathf.Clamp01(1 - flying.fly.visuals.localPosition.y / Mathf.Min(flying.fly.height_no_shadow, flying.fly.max_height)));

        if (flying.fly.visuals.transform.localPosition.y <= 0)
            is_complete = true;
    }

    public override State Next()
    {
        return flying.fly_dead;
    }

    public override void Exit()
    {
        is_complete = false;
    }

    public override void Setup(MonoBehaviour provider)
    {
        flying = provider as Flying;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDead : State
{
    protected Flying flying;
    public string clip_name;
    public float time_in_ground, fade_duration;

    bool faded;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        flying.animator.Play(clip_name);

        faded = false;
    }

    public override void Do()
    {
        if (!faded && time >= time_in_ground)
        {
            faded = true;
            flying.fade.enabled = true;
            flying.fade.start_alpha = 1;
            flying.fade.end_alpha = 0;
            flying.fade.time = fade_duration;
            flying.fade.Restart();

            FadeInOut shadow_fade = flying.shadow.GetComponent<FadeInOut>();
            shadow_fade.enabled = true;
            shadow_fade.start_alpha = 1;
            shadow_fade.end_alpha = 0;
            shadow_fade.time = fade_duration;
            shadow_fade.Restart();
        }
        else if (time >= time_in_ground + fade_duration)
        {
            flying.fly.target.parent = flying.transform;
            Destroy(flying.gameObject);
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
        flying = provider as Flying;
    }
}

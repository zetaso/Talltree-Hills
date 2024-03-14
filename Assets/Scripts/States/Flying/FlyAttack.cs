using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAttack : State
{
    protected Flying flying;
    public string clip_name;

    public float flying_time, fade_duration, time_to_vulnerable;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();

        flying.animator.Play(clip_name);
        flying.did_catch = true;

        if (flying.fly_anticipate.target_indicator != null)
        {
            Transform target = flying.fly_anticipate.target_indicator.transform;

            Vector2 attack_velocity = new Vector2(Vector2.Distance(target.position, flying.transform.position), flying.fly.visuals.localPosition.y);
            flying.rb.velocity = (target.position - flying.transform.position).normalized * attack_velocity.x / flying_time;
            flying.fly.y_velocity = -attack_velocity.y / flying_time;
        }

        is_complete = false;

        flying.fade.enabled = true;
        flying.fade.start_alpha = 0.1f;
        flying.fade.end_alpha = 1;
        flying.fade.time = fade_duration;
        flying.fade.Restart();
    }

    public override void Do()
    {
        flying.fly.visuals.localPosition += Vector3.up * flying.fly.y_velocity * Time.deltaTime;
        flying.shadow.color = new Color(1, 1, 1, Mathf.Clamp01(1 - flying.fly.visuals.localPosition.y / Mathf.Min(flying.fly.height_no_shadow, flying.fly.max_height)));

        if (time >= flying_time)
            is_complete = true;

        if (time >= time_to_vulnerable)
            flying.health.invincible = false;
    }

    public override State Next()
    {
        return flying.fly_catch;
    }

    public override void Exit()
    {
        is_complete = false;

        flying.transform.position = flying.fly_anticipate.target_indicator.transform.position;
        flying.fly.visuals.localPosition = Vector3.zero;
        flying.rb.velocity = Vector2.zero;
        flying.fly.y_velocity = 0;

        Destroy(flying.fly_anticipate.target_indicator);
    }

    public override void Setup(MonoBehaviour provider)
    {
        flying = provider as Flying;
    }
}

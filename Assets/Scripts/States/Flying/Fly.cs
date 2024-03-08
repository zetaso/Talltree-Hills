using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : State
{
    public Flying flying { get; private set; }

    public State fly_up, fly_down;
    public State state { get; private set; }

    public Transform visuals;
    public float speed, y_velocity, gravity;
    public float max_height, height_no_shadow, time_to_fade, fade_duration;
    public bool faded, invincible;

    Vector2 target_position;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();

        state = fly_up;
        state.Enter();

        flying.fly_safe.Trigger();

        is_complete = false;
        faded = false;
        invincible = false;

        target_position = new Vector2(Random.Range(-10, 10), Random.Range(-5, 5));
        flying.rb.velocity = speed * (Vector3)(target_position - (Vector2)transform.position).normalized;

        float angle = Vector3.SignedAngle(Vector3.right, flying.rb.velocity, Vector3.forward);
        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;
        flying.direction.SetDirection(angle / 360f);
    }

    public override void Do()
    {
        if (state.is_complete)
            GetNextState();

        state.Do();

        y_velocity -= gravity * Time.deltaTime;
        visuals.localPosition += Vector3.up * y_velocity * Time.deltaTime;

        flying.shadow.color = new Color(1, 1, 1, Mathf.Clamp01(1 - visuals.localPosition.y / Mathf.Min(height_no_shadow, max_height)));

        if (Vector2.Distance(flying.transform.position, target_position) < 0.1f)
            flying.rb.velocity = Vector2.zero;

        if (!faded && time >= time_to_fade)
        {
            faded = true;

            flying.fade.enabled = true;
            flying.fade.start_alpha = 1;
            flying.fade.end_alpha = 0.1f;
            flying.fade.time = fade_duration;
            flying.fade.Restart();
        }
        if (faded && !invincible && time >= time_to_fade + fade_duration)
        {
            invincible = true;
        }
    }

    public override State Next()
    {
        return null;
    }

    public override void Exit()
    {
        is_complete = false;
        flying.rb.velocity = Vector2.zero;
    }

    public override void Setup(MonoBehaviour provider)
    {
        flying = provider as Flying;
        fly_up.Setup(this);
        fly_down.Setup(this);

        state = fly_up;
    }

    void GetNextState()
    {
        State new_state = state.Next();
        if (new_state)
        {
            state.Exit();
            state = new_state;
            state.Enter();
        }
    }

    public void SetNextState(State new_state)
    {
        if (new_state)
        {
            state.Exit();
            state = new_state;
            state.Enter();
        }
    }
}

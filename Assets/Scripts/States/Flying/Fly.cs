using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fly : State
{
    public Flying flying { get; private set; }

    public State fly_up, fly_down;
    public State state { get; private set; }

    public Transform visuals;
    public float speed, y_velocity, gravity;
    public float max_height, height_no_shadow;

    public Transform target;
    public bool manual_position;
    public EventSequence onReachTarget;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();

        state = fly_up;
        state.Enter();

        flying.fly_safe.Trigger();

        is_complete = false;

        if (!flying.did_catch)
        {
            target.parent = null;
            if (!manual_position)
                target.position = new Vector2(Random.Range(-15, 15), Random.Range(-7, 7));
            else
                manual_position = false;
            flying.rb.velocity = speed * (target.position - transform.position).normalized;
        }
        else
        {
            target.parent = null;
            target.position = Utils.Warp(Quaternion.Euler(0, 0, Random.Range(-180, 180)) * Vector3.right) * 20f;
            flying.rb.velocity = speed * 3f * (target.position - transform.position).normalized;
        }

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

        if (target.parent != flying.transform && Vector2.Distance(flying.transform.position, target.position) < 0.1f)
        {
            if (onReachTarget)
            {
                onReachTarget.ExecuteEvent();
            }
            //if (onReachTarget != null)
            //{
            //    onReachTarget.Invoke();
            //    onReachTarget.RemoveAllListeners();
            //    onReachTarget = null;
            //}
            flying.rb.velocity = Vector2.zero;
            target.parent = flying.transform;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WalkerDie : State
{
    Walker walker;

    public string clip_name;
    public float time_to_dissapear;

    public UnityEvent on_die;

    public override void Trigger()
    {
        walker.SetNextState(this);
    }

    public override void Enter()
    {
        base.Enter();

        walker.lower_animator.Play(clip_name);
        walker.upper_renderer.enabled = false;
        is_complete = false;
    }

    public override void Do()
    {
        walker.rb.velocity = Vector2.zero;

        if (time >= time_to_dissapear)
        {
            Destroy(walker.gameObject);
            if (on_die != null)
                on_die.Invoke();
        }
    }

    public override State Next()
    {
        return null;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        walker = provider as Walker;
    }
}

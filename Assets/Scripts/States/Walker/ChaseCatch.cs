using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCatch : State
{
    Chase chase;

    public string lower_clip_name;
    public float catch_range;
    public GameObject head;
    public float time_to_kill;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        chase.walker.lower_animator.Play(lower_clip_name);
        chase.walker.upper_renderer.enabled = false;

        chase.walker.rb.velocity = Vector2.zero;
        chase.walker.rb.constraints = RigidbodyConstraints2D.FreezePosition;

        head.SetActive(true);
        head.transform.position = chase.walker.target.position - Vector3.up * 0.25f;

        Utils.Instance.player_health.SetDamaging(true);
        chase.walker.action.SetNextState(chase.walker.action.still);// make player character still and die animation
    }

    public override void Do()
    {
        //if (time >= time_to_kill)
        //    Utils.Instance.pause.Die();
    }

    public override State Next() { return null; }

    public override void Exit()
    {
        is_complete = false;
        chase.walker.upper_renderer.enabled = false;
        chase.walker.rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public override void Setup(MonoBehaviour provider)
    {
        chase = provider as Chase;
    }
}

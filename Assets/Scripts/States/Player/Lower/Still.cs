using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Still : State
{
    Movement movement;
    public string clip_name;

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        movement.animator.Play(clip_name);
        movement.rb.velocity = Vector2.zero;

        movement.action.GetComponent<SpriteRenderer>().enabled = false;
    }

    public override State Next()
    {
        return null;
    }

    public override void Exit()
    {
        is_complete = false;
        movement.action.GetComponent<SpriteRenderer>().enabled = true;
    }

    public override void Setup(MonoBehaviour provider)
    {
        movement = provider as Movement;
    }
}

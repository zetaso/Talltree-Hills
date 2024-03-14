using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendStand : State
{
    FriendMovement movement;

    public string clip_name;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        movement.animator.Play(clip_name);
    }

    public override void Do() { }

    public override State Next() { return null; }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        movement = provider as FriendMovement;
    }
}


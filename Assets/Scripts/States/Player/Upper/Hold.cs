using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : State
{
    [SerializeField] Movement movement;
    Action action;
    public string clip_name;

    public override void Trigger()
    {
        action.SetNextState(this);
    }

    public override void Enter()
    {
        action.animator.Play(clip_name);
        is_complete = true;
    }

    public override State Next()
    {
        if (Input.GetKey(KeyCode.R) && action.ammo < action.max_ammo)
            return action.reload;
        else if (movement.state == movement.run)
            return null;
        else if (Input.GetMouseButton(1))
            return action.aim;
        else
            return null;
    }

    public override void Setup(MonoBehaviour provider)
    {
        action = provider as Action;
    }
}

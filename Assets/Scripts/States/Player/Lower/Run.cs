using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : Walk
{
    [SerializeField] Action action;
    [SerializeField] Crosshair crosshair;

    public float animation_speed;
    public override void Enter()
    {
        base.Enter();
        movement.animator.speed = animation_speed;
        stamina.sprint.Trigger();

        if (action.state != action.reload && action.state != action.no_weapon)
            action.hold.Trigger();

        crosshair.inactive.Trigger();
        is_complete = true;
    }

    public override State Next()
    {
        if (movement.input == Vector2.zero)
            return movement.idle;
        else if (stamina.stamina == 0)
            return movement.walk;
        return null;
    }

    public override void Exit()
    {
        base.Exit();
        movement.animator.speed = 1;

        if (action.state != action.reload)
            crosshair.inactive.ForceExit();
    }
}

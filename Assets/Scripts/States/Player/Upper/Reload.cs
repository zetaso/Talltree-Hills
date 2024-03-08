using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : State
{
    [SerializeField] Crosshair crosshair;
    [SerializeField] Movement movement;
    Action action;
    public string clip_name;

    public float reload_duration;
    public float reload_crosshair_alpha;

    public override void Enter()
    {
        base.Enter();
        action.animator.Play(clip_name);

        is_complete = false;
        crosshair.inactive.Trigger();
    }

    public override void Do()
    {
        if (time >= reload_duration)
            is_complete = true;
    }

    public override State Next()
    {
        if (Input.GetMouseButton(1))
            return action.aim;
        else
            return action.hold;
    }

    public override void Exit()
    {
        base.Exit();

        action.ammo = action.max_ammo;
        action.ammoUI.SetAmmo(action.ammo);

        if (movement.state != movement.run)
            crosshair.inactive.ForceExit();
    }

    public override void Setup(MonoBehaviour provider)
    {
        action = provider as Action;
    }
}

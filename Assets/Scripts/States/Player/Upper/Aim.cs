using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : State
{
    [SerializeField] Crosshair crosshair;
    Action action;
    public string clip_name;

    public override void Enter()
    {
        action.animator.Play(clip_name);
        is_complete = true;
        action.ammoUI.KeepVisible();
    }

    public override void Do()
    {

        float angle = Vector3.SignedAngle(Vector3.right, crosshair.transform.position - Vector3.up * 1.625f - transform.position, Vector3.forward);

        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;

        action.direction.SetDirection(angle / 360f);
        action.ammoUI.KeepVisible();
    }

    public override State Next()
    {
        if (Input.GetMouseButton(0) && action.ammo > 0 && Utils.Instance.pause.CanInput())
            return action.shoot;
        else if (Input.GetKey(KeyCode.R) && action.ammo < action.max_ammo && action.ammo_left > 0 && Utils.Instance.pause.CanInput())
            return action.reload;
        else if (!Input.GetMouseButton(1) && Utils.Instance.pause.CanInput())
            return action.hold;
        return null;
    }

    public override void Exit()
    {
        base.Exit();
        action.ammoUI.KeepVisible();
    }

    public override void Setup(MonoBehaviour provider)
    {
        action = provider as Action;
    }
}

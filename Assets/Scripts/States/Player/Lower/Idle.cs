using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    [SerializeField] Stamina stamina;
    Movement movement;
    public string clip_name;

    public override void Enter()
    {
        movement.animator.Play(clip_name);
        is_complete = true;

        movement.rb.velocity = Vector2.zero;
        movement.action.animator.transform.localPosition = Vector3.zero;
    }

    public override State Next()
    {
        if (movement.input != Vector2.zero)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && stamina.stamina == 1)
                return movement.run;
            else
                return movement.walk;
        }
        return null;
    }

    public override void Exit()
    {
        is_complete = false;
    }

    public override void Setup(MonoBehaviour provider)
    {
        movement = provider as Movement;
    }
}

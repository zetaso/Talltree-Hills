using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : State
{
    [SerializeField] Action action;
    Crosshair crosshair;

    public float accuracy_lost, transition_duration;
    float transition_value, target_accuracy, old_accuracy;

    public override void Trigger()
    {
        crosshair.SetNextState(this);
    }

    public override void Enter()
    {
        is_complete = false;

        old_accuracy = crosshair.accuracy;
        target_accuracy = Mathf.Max(0, crosshair.accuracy - accuracy_lost);
        transition_value = 0;
    }

    public override void Do()
    {
        if (!Input.GetMouseButton(1))
            crosshair.SetNextState(crosshair.open);

        if (transition_value < 1)
        {
            transition_value = Mathf.Min(1, transition_value + Time.deltaTime / transition_duration);
            float eased_value = Utils.EaseOutQuart(transition_value);
            crosshair.accuracy = eased_value * target_accuracy + (1 - eased_value) * old_accuracy;

            if (transition_value == 1)
                is_complete = true;
        }
    }

    public override State Next()
    {
        if (action.state == action.aim || action.state == action.shoot)
            return crosshair.improve;
        else
            return crosshair.open;
    }

    public override void Exit()
    {
        is_complete = false;
    }

    public override void Setup(MonoBehaviour provider)
    {
        crosshair = provider as Crosshair;
    }
}

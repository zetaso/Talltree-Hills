using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : State
{
    [SerializeField] Action action;
    protected Crosshair crosshair;

    public float transition_duration;
    float transition_value, old_accuracy;

    public override void Enter()
    {
        is_complete = true;
        transition_value = 0;
        old_accuracy = crosshair.accuracy;
    }

    public override void Do()
    {
        if (transition_value < 1)
        {
            transition_value = Mathf.Min(1, transition_value + Time.deltaTime / transition_duration);
            float eased_value = Utils.EaseOutQuart(transition_value);
            crosshair.accuracy = (1 - eased_value) * old_accuracy;
        }
    }

    public override State Next()
    {
        if (action.state == action.aim)
            return crosshair.improve;
        return null;
    }

    public override void Exit()
    {
    }

    public override void Setup(MonoBehaviour provider)
    {
        crosshair = provider as Crosshair;
    }
}

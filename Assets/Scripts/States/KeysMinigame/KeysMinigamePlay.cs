using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysMinigamePlay : State
{
    KeysMinigame minigame;
    public float initial_value, value_per_key, decrease_rate;

    float value;
    bool left_key, first_key = true;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        value = initial_value;
    }

    public override void Do()
    {
        value = Mathf.Max(0, value - decrease_rate * Time.deltaTime);

        bool a_pressed = Input.GetKeyDown(KeyCode.A);
        bool d_pressed = Input.GetKeyDown(KeyCode.D);

        if (a_pressed && (left_key || first_key))
        {
            if (first_key)
            {
                first_key = false;
                left_key = true;
            }
            left_key = !left_key;
            value += value_per_key;
        }
        else if (d_pressed && (!left_key || first_key))
        {
            if (first_key)
            {
                first_key = false;
                left_key = false;
            }
            left_key = !left_key;
            value += value_per_key;
        }

        if (value >= 1)
            is_complete = true;
    }

    public override State Next()
    {
        return minigame.vanish;
    }

    public override void Exit()
    {
        is_complete = false;
    }

    public override void Setup(MonoBehaviour provider)
    {
        minigame = provider as KeysMinigame;
    }
}

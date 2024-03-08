using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldState : MonoBehaviour
{
    public List<string> all_states;
    Dictionary<string, float> states;

    void Awake()
    {
        states = new Dictionary<string, float>();
        for (int i = 0; i < all_states.Count; i++)
        {
            states.Add(all_states[i], 0f);
        }
    }

    public bool TrySetState(string state, float value)
    {
        if (states.ContainsKey(state))
        {
            states[state] = value;
            return true;
        }
        else
            return false;
    }

    public bool TrySetState(string state, bool value)
    {
        if (states.ContainsKey(state))
        {
            states[state] = value ? 1f : 0f;
            return true;
        }
        else
            return false;
    }

    public float GetState(string state)
    {
        if (states.ContainsKey(state))
            return states[state];
        else
            return 0f;
    }

    public bool GetStateBool(string state)
    {
        if (states.ContainsKey(state))
            return states[state] > 0f;
        else
            return false;
    }

    public bool IsState(string state)
    {
        if (states.ContainsKey(state))
            return states[state] > 0f;
        else
            return false;
    }
}

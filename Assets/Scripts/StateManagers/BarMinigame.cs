using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMinigame : MonoBehaviour
{
    public State appear, vanish, move, fail, success, reposition;
    public State state { get; private set; }

    public Escape escape;
    public SpriteRenderer bar, cursor, area;
    public FadeInOut fade, cursor_and_area_fade;
    public int attemps;

    void Start()
    {
        appear.Setup(this);
        vanish.Setup(this);
        move.Setup(this);
        fail.Setup(this);
        success.Setup(this);
        reposition.Setup(this);

        state = appear;
        state.Enter();
    }

    void Update()
    {
        if (state.is_complete)
            GetNextState();

        state.Do();
    }

    void GetNextState()
    {
        State new_state = state.Next();
        if (new_state)
        {
            state.Exit();
            state = new_state;
            state.Enter();
        }
    }

    public void SetNextState(State new_state)
    {
        if (new_state)
        {
            state.Exit();
            state = new_state;
            state.Enter();
        }
    }
}

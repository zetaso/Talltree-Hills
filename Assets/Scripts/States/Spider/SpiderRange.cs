using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRange : State
{
    public Spider spider { get; private set; }
    public SpiderRangeAnticipate anticipate;
    public SpiderRangeJump jump;
    public SpiderRangeHug hug;
    public State state { get; private set; }

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();

        state = anticipate;
        state.Enter();
    }

    public override void Do()
    {
        if (state.is_complete)
            GetNextState();

        state.Do();
    }

    public override State Next() { return null; }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        spider = provider as Spider;
        anticipate.Setup(this);
        jump.Setup(this);
        hug.Setup(this);
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

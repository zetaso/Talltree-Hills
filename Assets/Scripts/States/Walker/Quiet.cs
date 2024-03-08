using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiet : State
{
    public QuietIdle idle;
    public QuietWalk walk;
    public State state { get; private set; }

    public Walker walker { get; private set; }

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        state = idle;
        state.Enter();
    }

    public override void Do()
    {
        if (state.is_complete)
            GetNextState();

        state.Do();

        if (Vector2.Scale(walker.transform.position - walker.target.position, new Vector2(1, 2)).magnitude <= walker.chase.vision_range)
            is_complete = true;
    }

    public override State Next()
    {
        return walker.chase;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        walker = provider as Walker;

        idle.Setup(this);
        walk.Setup(this);
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
}

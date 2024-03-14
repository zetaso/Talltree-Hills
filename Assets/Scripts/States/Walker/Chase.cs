using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    public ChaseIdle idle;
    public ChaseRun run;
    public ChaseCatch catch_;
    public State state { get; private set; }

    public Walker walker { get; private set; }

    public float vision_range, idle_timeout, time_since_idle;

    public override void Trigger()
    {
        walker.SetNextState(this);
    }

    public override void Enter()
    {
        base.Enter();

        state = run;
        state.Enter();
        is_complete = false;
    }

    public override void Do()
    {
        if (state.is_complete)
            GetNextState();

        state.Do();

        if (state == idle)
        {
            time_since_idle += Time.deltaTime;
            if (time_since_idle >= idle_timeout)
                is_complete = true;
        }
    }

    public override State Next()
    {
        return walker.quiet;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        walker = provider as Walker;

        idle.Setup(this);
        run.Setup(this);
        catch_.Setup(this);
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

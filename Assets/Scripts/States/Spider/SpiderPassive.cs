using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPassive : State
{
    public Spider spider { get; private set; }
    public SpiderIdle idle;
    public SpiderWalk walk;
    public State state { get; private set; }

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

        if (Vector2.Scale(spider.target.position - spider.transform.position, new Vector2(1, 2)).magnitude <= spider.vision_range)
            is_complete = true;
    }

    public override State Next()
    {
        return spider.chase;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        spider = provider as Spider;

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

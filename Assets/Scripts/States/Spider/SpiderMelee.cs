using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMelee : State
{
    public Spider spider { get; private set; }
    public SpiderMeleeAnticipate anticipate;
    public SpiderMeleeAttack attack;
    public State state { get; private set; }

    public float attack_range;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();

        state = anticipate;
        state.Enter();

        Vector2 direction = (spider.target.position - spider.transform.position).normalized;
        float angle = Vector3.SignedAngle(Vector3.right, Vector2.Scale(direction, new Vector2(1, 2)), Vector3.forward);
        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;
        spider.direction.SetDirection(angle / 360f);
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
        attack.Setup(this);
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

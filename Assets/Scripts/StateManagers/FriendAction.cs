using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendAction : MonoBehaviour
{
    public FriendIdle idle;
    public FriendBackpack backpack;
    public State state { get; private set; }

    public Animator animator;

    void Start()
    {
        idle.Setup(this);
        backpack.Setup(this);

        state = idle;
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

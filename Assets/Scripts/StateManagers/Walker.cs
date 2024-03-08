using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    public Chase chase;
    public Quiet quiet;
    public State state { get; private set; }

    public Rigidbody2D rb;
    public Animator upper_animator, lower_animator;
    public Direction direction;
    public Transform target;

    void Start()
    {
        chase.Setup(this);
        quiet.Setup(this);

        state = quiet;
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

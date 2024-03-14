using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    public Chase chase;
    public Quiet quiet;
    public WalkerDie die;
    public State state { get; private set; }

    public Rigidbody2D rb;
    public Animator upper_animator, lower_animator;
    public SpriteRenderer upper_renderer, lower_renderer;
    public Direction direction;
    public Transform target;
    public Action action;
    public Health health;

    void Start()
    {
        chase.Setup(this);
        quiet.Setup(this);
        die.Setup(this);

        state = quiet;
        state.Enter();

        health.onDie += die.Trigger;
    }

    void Update()
    {
        if (state.is_complete)
            GetNextState();

        state.Do();

        if (state != chase && (state != quiet || quiet.state != quiet.walk))
            rb.velocity = Vector2.zero;
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

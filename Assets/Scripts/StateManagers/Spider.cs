using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public SpiderFall fall;
    public SpiderLand land;
    public SpiderPassive passive;
    public SpiderChase chase;
    public SpiderMelee melee;
    public SpiderRange range;
    public State state { get; private set; }

    public Rigidbody2D rb;
    public Animator animator;
    public Direction direction;
    public Transform visuals;
    public Transform target;

    public Vector2 origin_position;
    public float vision_range;

    void Start()
    {
        fall.Setup(this);
        land.Setup(this);
        passive.Setup(this);
        chase.Setup(this);
        melee.Setup(this);
        range.Setup(this);

        state = fall;
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
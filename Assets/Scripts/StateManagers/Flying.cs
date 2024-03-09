using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    public Fly fly;
    public FlyAnticipate fly_anticipate;
    public FlyAttack fly_attack;
    public FlyCatch fly_catch;
    public FlyFall fly_fall;
    public FlyDead fly_dead;
    public State state;

    public FlySafe fly_safe;
    public FlyAggro fly_aggro;
    public State mode;


    public Animator animator;
    public Direction direction;
    public SpriteRenderer shadow;

    public FadeInOut fade;
    public Health health;
    public Rigidbody2D rb;
    public Transform player;

    void Start()
    {
        fly_safe.Setup(this);
        fly_aggro.Setup(this);

        mode = fly_safe;
        mode.Enter();

        fly.Setup(this);
        fly_anticipate.Setup(this);
        fly_attack.Setup(this);
        fly_catch.Setup(this);
        fly_fall.Setup(this);
        fly_dead.Setup(this);

        state = fly;
        state.Enter();

        health.onDie += fly_fall.Trigger;
    }

    void Update()
    {
        if (mode.is_complete)
            GetNextState(ref mode);

        mode.Do();

        if (state.is_complete)
            GetNextState(ref state);

        state.Do();

        if (Input.GetKeyDown(KeyCode.K))
            health.TakeDamage(1);
    }

    void GetNextState(ref State _state)
    {
        State new_state = _state.Next();
        if (new_state)
        {
            _state.Exit();
            _state = new_state;
            _state.Enter();
        }
    }

    public void SetNextState(ref State _state, State new_state)
    {
        if (new_state)
        {
            _state.Exit();
            _state = new_state;
            _state.Enter();
        }
    }
}

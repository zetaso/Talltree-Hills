using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public State idle, walk, run;
    public State state { get; private set; }

    public Action action;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer ren;
    public Direction direction;
    public StaminaUI staminaUI;

    public Vector2 input { get; private set; }

    void Start()
    {
        idle.Setup(this);
        walk.Setup(this);
        run.Setup(this);

        state = idle;
        state.Enter();
    }

    void Update()
    {
        ReadInput();

        if (state.is_complete)
            GetNextState();

        state.Do();

        if (state != walk && state != run)
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

    void ReadInput()
    {
        input = (!Utils.Instance.pause.CanInput() ||
        action.state == action.escape ||
        action.state == action.fight_spider ||
        action.state == action.still)
         ? Vector2.zero : new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    public void SetDirection(float angle)
    {
        direction.SetDirection(angle / 360f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public State full, sprint, exhaust, recover;
    public State state { get; private set; }

    [SerializeField] StaminaUI ui;
    public float stamina;
    public float alpha = 1;

    void Start()
    {
        full.Setup(this);
        sprint.Setup(this);
        exhaust.Setup(this);
        recover.Setup(this);

        state = full;
        state.Enter();
    }

    void Update()
    {

        if (state.is_complete)
            GetNextState();

        state.Do();

        ui.SetStamina(stamina);
        ui.SetAlpha(alpha);
        ui.KeepVisible();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysMinigame : MonoBehaviour
{
    public State appear, play, vanish, die;
    public State state { get; private set; }

    public FightSpider fight_spider;
    public FadeInOut fade;

    void Start()
    {
        appear.Setup(this);
        play.Setup(this);
        vanish.Setup(this);
        die.Setup(this);

        state = null;
    }

    void Update()
    {
        if (state)
        {
            if (state.is_complete)
                GetNextState();

            state.Do();
        }
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
            if (state)
                state.Exit();
            state = new_state;
            state.Enter();
        }
    }
}

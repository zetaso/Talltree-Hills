using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public State open, improve, recoil;
    public Inactive inactive;
    public State state { get; private set; }

    public Transform right, up, left, down;
    public float max_pixels, min_pixels;

    public float accuracy; //  0: full open crosshair
                           //  1: full closed crosshair

    void Start()
    {
        open.Setup(this);
        inactive.Setup(this);
        improve.Setup(this);
        recoil.Setup(this);

        state = open;
        state.Enter();
    }

    void Update()
    {
        if (state.is_complete)
            GetNextState();

        state.Do();

        SetLines(accuracy);
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

    public void SetLines(float value)
    {
        right.localPosition = Vector3.right * (value * min_pixels + (1 - value) * max_pixels) / 8f;
        up.localPosition = Vector3.up * (value * min_pixels + (1 - value) * max_pixels) / 8f;
        left.localPosition = Vector3.left * (value * min_pixels + (1 - value) * max_pixels) / 8f;
        down.localPosition = Vector3.down * (value * min_pixels + (1 - value) * max_pixels) / 8f;
    }

    public void SetAlpha(float value)
    {
        Color new_color = Utils.Instance.palette[(int)ColorTag.WHITE];
        new_color.a = value;

        right.GetComponent<SpriteRenderer>().color = new_color;
        up.GetComponent<SpriteRenderer>().color = new_color;
        left.GetComponent<SpriteRenderer>().color = new_color;
        down.GetComponent<SpriteRenderer>().color = new_color;
    }
}

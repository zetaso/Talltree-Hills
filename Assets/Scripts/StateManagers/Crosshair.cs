using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public State open, improve, recoil;
    public Inactive inactive;
    public State state { get; private set; }

    public Action action;
    public Transform right, up, left, down;
    public float max_pixels, min_pixels;

    public float min_dist, max_dist;                    // accuracy curve x limits
    public float min_dist_accuracy, max_dist_accuracy;  // accuracy curve y limits
    public float max_base_accuracy;

    public float accuracy;  //  0: full open crosshair
                            //  1: full closed crosshair

    public float min_accuracy;

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

        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float base_accuracy;
        float distance = Vector2.Distance(transform.position, action.transform.position);
        if (distance <= min_dist)
            base_accuracy = 0;
        else if (distance >= max_dist)
            base_accuracy = max_base_accuracy;
        else
        {
            float local_x = (distance - min_dist) / (max_dist - min_dist);
            float local_y = Utils.EaseInSine(local_x);
            base_accuracy = local_y * max_base_accuracy;
        }

        SetLines(base_accuracy + accuracy);
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

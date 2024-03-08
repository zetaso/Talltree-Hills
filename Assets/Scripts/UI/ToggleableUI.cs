using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleableUI : MonoBehaviour
{
    [SerializeField]
    FloatingUI[] floatingUIs;

    public float vanish_time, appear_time;
    public float time_to_vanish;

    float time_since_interaction;
    float alpha;
    bool vanishing, appearing;

    void Awake()
    {
        foreach (var item in floatingUIs)
            item.SetToggleableUI(this);
    }

    void Update()
    {
        time_since_interaction += Time.deltaTime;
        if (time_since_interaction >= time_to_vanish)
            vanishing = true;

        if (vanishing)
        {
            alpha = Mathf.Max(0, alpha - Time.deltaTime / vanish_time);
            if (alpha == 0)
                vanishing = false;

            SetAlpha(alpha);
        }

        if (appearing)
        {
            alpha = Mathf.Min(1, alpha + Time.deltaTime / appear_time);
            if (alpha == 1)
                appearing = false;

            SetAlpha(alpha);
        }
    }

    public void OnInteract()
    {
        time_since_interaction = 0;
        appearing = false;
        vanishing = false;

        if (alpha != 1)
            appearing = true;
    }

    void SetAlpha(float value)
    {
        foreach (var item in floatingUIs)
            item.SetAlpha(value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Transform right, up, left, down;
    public float max_pixels, min_pixels;
    public float transition_duration;

    float transition_value;

    float accuracy; //  0: full open crosshair
                    //  1: full closed crosshair
    float target_accuracy, old_accuracy;

    void Update()
    {
        if (transition_value < 1)
        {
            transition_value = Mathf.Min(1, transition_value + Time.deltaTime / transition_duration);
            float eased_value = Ease(transition_value);
            accuracy = eased_value * target_accuracy + (1 - eased_value) * old_accuracy;
        }

        right.localPosition = Vector3.right * (accuracy * min_pixels + (1 - accuracy) * max_pixels) / 8f;
        up.localPosition = Vector3.up * (accuracy * min_pixels + (1 - accuracy) * max_pixels) / 8f;
        left.localPosition = Vector3.left * (accuracy * min_pixels + (1 - accuracy) * max_pixels) / 8f;
        down.localPosition = Vector3.down * (accuracy * min_pixels + (1 - accuracy) * max_pixels) / 8f;
    }

    public void TransitionAccuracy(float value)
    {
        old_accuracy = accuracy;
        target_accuracy = value;
        transition_value = 0;
    }

    public void UpdateAccuracy(float value)
    {
        target_accuracy = value;
        accuracy = value;
    }

    float Ease(float x)
    {
        return 1 - Mathf.Pow(1 - x, 4);
    }
}

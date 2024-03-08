using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    public float start_alpha, end_alpha, time, pow;
    public float current_alpha { get; private set; }

    float lerp_value = 0;

    void Start()
    {
        current_alpha = start_alpha;
        SetAlpha(current_alpha);
    }

    void Update()
    {
        lerp_value = Mathf.Min(1, lerp_value + Time.deltaTime / time);
        current_alpha = Mathf.Lerp(start_alpha, end_alpha, lerp_value);
        SetAlpha(Mathf.Pow(current_alpha, pow));

        if (lerp_value == 1)
            enabled = false;
    }

    void SetAlpha(float value)
    {
        Color color = spriteRenderer.color;
        color.a = value;
        spriteRenderer.color = color;
    }

    public void Restart()
    {
        lerp_value = 0;
    }
}
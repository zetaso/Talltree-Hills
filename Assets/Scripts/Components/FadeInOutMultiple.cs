using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutMultiple : FadeInOut
{
    public SpriteRenderer[] spriteRenderers;

    public override void SetAlpha(float value)
    {
        foreach (var item in spriteRenderers)
        {
            Color color = item.color;
            color.a = value;
            item.color = color;
        }
    }
}

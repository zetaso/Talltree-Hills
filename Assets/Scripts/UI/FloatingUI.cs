using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    public List<SpriteRenderer> sprites;

    ToggleableUI toggleableUI;

    public void SetAlpha(float a)
    {
        foreach (var item in sprites)
        {
            Color new_color = item.color;
            new_color.a = a;
            item.color = new_color;
        }
    }

    public void KeepVisible()
    {
        toggleableUI.OnInteract();
    }

    public void SetToggleableUI(ToggleableUI toggleableUI)
    {
        this.toggleableUI = toggleableUI;
    }
}

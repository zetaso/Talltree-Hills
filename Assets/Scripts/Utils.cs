using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorTag
{
    BLACK,
    DARK_LILA,
    LILA,
    MAGENTA,
    LIGHT_PINK,
    SKIN,
    WHITE
}

public class Utils : MonoBehaviour
{
    public Color[] palette;
    public static Utils Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }
}

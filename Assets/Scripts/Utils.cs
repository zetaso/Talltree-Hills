using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public Material default_material, white_material;
    bool cursor_visibility;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }


    void Start()
    {
        Cursor.visible = cursor_visibility;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursor_visibility = !cursor_visibility;
            Cursor.visible = cursor_visibility;
        }
    }
    public static IEnumerator Delay(float time, UnityAction action)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }

    public static float EaseInSine(float x)
    {
        return 1 - Mathf.Cos(x * Mathf.PI / 2f);
    }

    public static float EaseOutQuart(float x)
    {
        return 1 - Mathf.Pow(1 - x, 4);
    }

    public static Vector2 Warp(Vector2 input)
    {
        Vector2 unit = input.normalized;
        return input / new Vector2(unit.x, unit.y * 1.414f).magnitude;
    }

    public static Vector2 Unwarp(Vector2 input)
    {
        Vector2 unit = input.normalized;
        return input * new Vector2(unit.x, unit.y * 0.707f).magnitude;
    }
}

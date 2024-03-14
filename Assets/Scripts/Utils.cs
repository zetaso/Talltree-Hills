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
    public string[] scene_names;
    public int last_gate_crossed;

    public Sprite[] talker_profiles;
    public Color[] palette;
    public static Utils Instance { get; private set; }
    public PauseListener pause;
    public SceneDataManager scene_data_manager;
    public Interact interact;

    public PlayerHealth player_health;

    public static float unpausedDeltaTime;
    public static float dialogDeltaTime;
    public static float minigameDeltaTime;
    public Material default_material, white_material, outline_material;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (pause)
        {
            dialogDeltaTime = (pause.paused || pause.dead) ? 0 : Time.unscaledDeltaTime;
            unpausedDeltaTime = (pause.paused || pause.dialog) ? 0 : Time.unscaledDeltaTime;
            minigameDeltaTime = pause.paused ? 0 : Time.unscaledDeltaTime;
        }
    }

    public static void SetCursorVisibility(bool visibility)
    {
        Cursor.visible = visibility;
    }

    public static IEnumerator Delay(float time, UnityAction action)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }

    public static float easeInOutSine(float x)
    {
        return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
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

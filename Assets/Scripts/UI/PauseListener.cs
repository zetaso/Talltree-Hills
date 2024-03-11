using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PauseListener : MonoBehaviour
{
    public bool paused, dead;
    public UnityEvent onPause, onUnpause;

    public GameObject main_camera, bokeh_camera;
    public Action action;

    void Start()
    {
        OnPauseToggle();
    }

    void Update()
    {
        if (!dead && Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            OnPauseToggle();
        }
    }

    public void Unpause()
    {
        paused = false;
        OnPauseToggle();
    }

    public void OnPauseToggle()
    {
        if (paused)
        {
            Time.timeScale = 0;
            onPause.Invoke();
        }
        else
        {
            if (action.state != action.escape)
                Time.timeScale = 1;
            onUnpause.Invoke();
        }
    }
}

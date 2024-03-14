using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PauseListener : MonoBehaviour
{
    public bool paused, dialog, dead, cinematic;
    public UnityEvent onPause, onUnpause;

    public GameObject main_camera, bokeh_camera;
    public Action action;

    public GameObject die_pannel;

    void Start()
    {
        OnPauseToggle();
    }

    void Update()
    {
        if (!dead && !cinematic && Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            OnPauseToggle();
        }
    }

    public void Die()
    {
        dead = true;
        Time.timeScale = 0;
        StartCoroutine(DieAfter(1.5f));
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
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
            Cursor.visible = true;
        }
        else
        {
            if (action.state != action.escape)
                Time.timeScale = 1;
            onUnpause.Invoke();
            Cursor.visible = false;
        }
    }

    public bool CanInput()
    {
        return !paused && !dialog && !dead && !cinematic;
    }

    public void SetCinematic(bool c)
    {
        cinematic = c;
    }

    public IEnumerator DieAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        die_pannel.SetActive(true);
        Cursor.visible = true;
    }
}

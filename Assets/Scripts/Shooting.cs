using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Crosshair crosshair;
    public float aiming_accuracy_time;

    PlayerState state;
    bool cursor_visibility = false;
    float accuracy;

    void Start()
    {
        state = GetComponent<PlayerState>();
        Cursor.visible = cursor_visibility;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursor_visibility = !cursor_visibility;
            Cursor.visible = cursor_visibility;
        }

        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crosshair.transform.position = (Vector3)mouse;

        bool aiming = Input.GetMouseButton(1);
        bool on_aim = !state.GetStateBool("aiming") && aiming;
        bool on_stop_aim = state.GetStateBool("aiming") && !aiming;

        if (on_aim || on_stop_aim)
        {
            accuracy = 0;
            crosshair.TransitionAccuracy(accuracy);
        }
        else if (aiming)
        {
            accuracy = Mathf.Min(1, accuracy + Time.deltaTime / aiming_accuracy_time);
            crosshair.UpdateAccuracy(accuracy);

            float angle = Vector3.SignedAngle(Vector3.right, crosshair.transform.position - Vector3.up * 1.625f - transform.position, Vector3.forward);

            if (angle < 0)
                angle += 360f;
            else if (angle > 360f)
                angle -= 360f;

            state.TrySetState("direction", angle / 360f);
        }

        bool on_shoot = aiming && Input.GetMouseButton(0);

        state.TrySetState("aiming", aiming);
        state.TrySetState("on_shoot", on_shoot);
    }
}

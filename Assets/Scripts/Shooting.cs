using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public int max_ammo;
    public Crosshair crosshair;
    public GameObject shoot_flash;
    public float aiming_accuracy_time;
    public float accuracy_lose_on_shoot;
    public AmmoUI ammoUI;

    PlayerState state;
    bool cursor_visibility = false;
    float accuracy;
    int ammo;

    void Start()
    {
        state = GetComponent<PlayerState>();
        Cursor.visible = cursor_visibility;
        ammo = max_ammo;
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
            ammoUI.KeepVisible();
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
            ammoUI.KeepVisible();
        }

        bool shooting = state.GetStateBool("shooting");
        bool on_shoot = aiming && !shooting && ammo > 0 && Input.GetMouseButtonDown(0);

        if (on_shoot)
        {
            accuracy = Mathf.Max(0, accuracy - accuracy_lose_on_shoot);
            crosshair.TransitionAccuracy(accuracy);
            Transform flash = Instantiate(shoot_flash, transform.position, Quaternion.identity).transform;
            int flash_index = (int)(state.GetState("direction") * 8f + 0.5f);

            if (flash_index > 7)
                flash_index -= 8;
            else if (flash_index < 0)
                flash_index += 8;

            flash.GetChild(flash_index).gameObject.SetActive(true);
            Destroy(flash.gameObject, 0.125f);

            ammo--;
            ammoUI.SetAmmo(ammo);
            ammoUI.KeepVisible();
        }

        state.TrySetState("aiming", aiming);
        state.TrySetState("on_shoot", on_shoot);
    }
}

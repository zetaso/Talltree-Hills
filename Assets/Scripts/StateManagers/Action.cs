using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Action : MonoBehaviour
{
    public Transform sphere;
    public State hold, aim, reload;
    public Shoot shoot;
    public State state { get; private set; }

    public Animator animator;
    public Direction direction;

    [Header("Aiming")]
    public Crosshair crosshair;

    [Header("Shooting")]
    public int max_ammo, ammo;
    public AmmoUI ammoUI;
    public GameObject shoot_flash;

    void Start()
    {
        hold.Setup(this);
        aim.Setup(this);
        shoot.Setup(this);
        reload.Setup(this);

        state = hold;
        state.Enter();

        ammo = max_ammo;
    }

    void Update()
    {
        ReadInput();

        if (state.is_complete)
        {
            GetNextState();
        }

        state.Do();
    }

    void GetNextState()
    {
        State new_state = state.Next();
        if (new_state)
        {
            state.Exit();
            state = new_state;
            state.Enter();
        }
    }

    public void SetNextState(State new_state)
    {
        if (new_state)
        {
            state.Exit();
            state = new_state;
            state.Enter();
        }
    }

    void ReadInput()
    {
    }


    //void Update()
    //{
    //    bool on_reload = !state.GetStateBool("shooting") && !state.GetStateBool("reloading") && ammo < 7 && Input.GetKeyDown(KeyCode.R);
    //
    //    bool aiming = Input.GetMouseButton(1);
    //    bool on_aim = !state.GetStateBool("aiming") && aiming;
    //    bool on_stop_aim = state.GetStateBool("aiming") && !aiming;
    //

    //    else if (aiming)
    //    {
    //        if (crosshair.transition_value == 1)
    //        {
    //            accuracy_lerp_value = Mathf.Min(1, accuracy_lerp_value + Time.deltaTime / aiming_accuracy_time);
    //            float better_accuracy = Utils.EaseInSine(accuracy_lerp_value);
    //            accuracy = crosshair.UpdateAccuracy(better_accuracy);
    //        }
    //
    //        float angle = Vector3.SignedAngle(Vector3.right, crosshair.transform.position - Vector3.up * 1.625f - transform.position, Vector3.forward);
    //
    //        if (angle < 0)
    //            angle += 360f;
    //        else if (angle > 360f)
    //            angle -= 360f;
    //
    //        state.TrySetState("direction", angle / 360f);
    //        ammoUI.KeepVisible();
    //    }
    //
    //    bool shooting = state.GetStateBool("shooting");
    //    bool on_shoot = aiming && !shooting && ammo > 0 && Input.GetMouseButtonDown(0);
    //
    //    if (on_shoot)
    //    {
    //        accuracy_lerp_value = Mathf.Max(0, accuracy_lerp_value - accuracy_lerp_value);
    //        accuracy = Utils.EaseInSine(accuracy_lerp_value);
    //        crosshair.TransitionAccuracy(accuracy, true);
    //        Transform flash = Instantiate(shoot_flash, transform.position, Quaternion.identity).transform;
    //        int flash_index = (int)(state.GetState("direction") * 8f + 0.5f);
    //
    //        if (flash_index > 7)
    //            flash_index -= 8;
    //        else if (flash_index < 0)
    //            flash_index += 8;
    //
    //        flash.GetChild(flash_index).gameObject.SetActive(true);
    //        Destroy(flash.gameObject, 0.125f);
    //
    //        ammo--;
    //        ammoUI.SetAmmo(ammo);
    //        ammoUI.KeepVisible();
    //    }
    //
    //    state.TrySetState("aiming", aiming);
    //    state.TrySetState("on_shoot", on_shoot);
    //    state.TrySetState("on_reload", on_reload);
    //}
    //
    //public void OnEndReload()
    //{
    //    ammo = max_ammo;
    //    ammoUI.SetAmmo(ammo);
    //}
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Action : MonoBehaviour
{
    public Hold hold;
    public Aim aim;
    public Reload reload;
    public Escape escape;
    public FightSpider fight_spider;
    public NoWeapon no_weapon;
    public Shoot shoot;
    public Still still;
    public State state { get; private set; }

    public Movement movement;
    public Animator animator;
    public Direction direction;

    [Header("Shooting")]
    public int max_ammo, ammo, ammo_left;
    public TMP_Text ammo_text;
    public AmmoUI ammoUI;
    public GameObject shoot_flash;

    void Start()
    {
        hold.Setup(this);
        aim.Setup(this);
        shoot.Setup(this);
        reload.Setup(this);
        escape.Setup(this);
        fight_spider.Setup(this);
        no_weapon.Setup(this);
        still.Setup(this);

        state = no_weapon;
        state.Enter();

        ammo_text.text = ammo + " / " + ammo_left;
        ammoUI.SetAmmo(ammo);
    }

    void Update()
    {
        if (state.is_complete)
            GetNextState();

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

    public void AddAmmo(int amount)
    {
        ammo_left += amount;
        ammo_text.text = ammo + " / " + ammo_left;
    }

    public void ReloadAmmo()
    {
        ammo = Mathf.Min(ammo_left, 7);
        ammo_left -= ammo;
        ammo_text.text = ammo + " / " + ammo_left;
    }

    public void OnShootAmmo()
    {
        ammo--;
        ammo_text.text = ammo + " / " + ammo_left;
    }

    public void SetWeapon()
    {
        SetNextState(hold);
    }
}

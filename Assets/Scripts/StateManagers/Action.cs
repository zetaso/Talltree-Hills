using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Action : MonoBehaviour
{
    public Transform sphere;
    public Hold hold;
    public Aim aim;
    public Reload reload;
    public Escape escape;
    public FightSpider fight_spider;
    public Shoot shoot;
    public State state { get; private set; }

    public Movement movement;
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
        escape.Setup(this);
        fight_spider.Setup(this);

        state = hold;
        state.Enter();

        ammo = max_ammo;
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
}

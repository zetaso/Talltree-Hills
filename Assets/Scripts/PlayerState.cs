using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{
    public Animator legs, torso;

    void Start()
    {

    }

    void Update()
    {
        if (IsState("moving"))
        {
        }
    }
}

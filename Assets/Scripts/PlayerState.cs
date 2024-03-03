using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{
    public Animator legs, torso;
    public SpriteRenderer legs_ren;

    void Start()
    {
        legs_ren = legs.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        legs.SetFloat("direction", GetState("direction"));
        torso.SetFloat("direction", GetState("direction"));

        if (IsState("moving"))
        {
            legs.Play("walk");
            string name = legs_ren.sprite.name;
            string number = name.Substring(name.Length - 2, 2);
            if (number[0] == '_')
                number = number[1].ToString();
            int index = int.Parse(number);
            if (index % 2 == 1)
                torso.transform.localPosition = Vector3.zero;
            else if (index % 4 == 0)
                torso.transform.localPosition = Vector3.up * 0.125f;
            else
                torso.transform.localPosition = Vector3.down * 0.125f;
        }
        else
        {
            legs.Play("idle");
            torso.transform.localPosition = Vector3.zero;
        }

        if (IsState("aiming"))
        {
            torso.Play("aim");
        }
        else
        {
            torso.Play("hold");
        }
    }
}

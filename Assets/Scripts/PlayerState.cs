using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerState : State
{
    public float running_anim_speed;
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
            if (IsState("running"))
                legs.speed = running_anim_speed;
            else
                legs.speed = 1;

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

        if (IsState("on_shoot"))
        {
            TrySetState("shooting", true);
            torso.Play("shoot");
            TrySetState("on_shoot", false);
            StartCoroutine(Delay(0.375f, () => { TrySetState("shooting", false); }));
        }

        if (IsState("shooting"))
        {

        }
        else if (IsState("aiming"))
        {
            torso.Play("aim");
        }
        else
        {
            torso.Play("hold");
        }
    }

    public static IEnumerator Delay(float time, UnityAction action)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }
}

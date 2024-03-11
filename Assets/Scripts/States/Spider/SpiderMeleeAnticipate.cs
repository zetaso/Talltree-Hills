using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMeleeAnticipate : State
{
    SpiderMelee melee;

    public string clip_name;
    public float anticipate_time;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        melee.spider.animator.Play(clip_name);
    }

    public override void Do()
    {
        melee.spider.rb.velocity = Vector2.zero;
        if (time >= anticipate_time)
            is_complete = true;
    }

    public override State Next()
    {
        return melee.attack;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        melee = provider as SpiderMelee;
    }
}

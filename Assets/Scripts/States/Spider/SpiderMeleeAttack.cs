using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMeleeAttack : State
{
    SpiderMelee melee;

    public string clip_name;
    public float attack_duration;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        melee.spider.animator.Play(clip_name);
    }

    public override void Do()
    {
        if (time >= attack_duration)
            melee.spider.SetNextState(melee.spider.chase);
    }

    public override State Next() { return null; }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        melee = provider as SpiderMelee;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderIdle : State
{
    SpiderPassive passive;

    public string clip_name;
    public float idle_timeout;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = true;

        passive.spider.animator.Play(clip_name, 0, 0);
        passive.spider.animator.speed = 0;
    }

    public override void Do() { }

    public override State Next()
    {
        if (time >= idle_timeout)
            return passive.walk;
        return null;
    }

    public override void Exit()
    {
        is_complete = false;
        passive.spider.animator.speed = 1;
    }

    public override void Setup(MonoBehaviour provider)
    {
        passive = provider as SpiderPassive;
    }
}

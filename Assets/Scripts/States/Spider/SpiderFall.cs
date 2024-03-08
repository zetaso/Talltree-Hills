using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFall : State
{
    Spider spider;

    public string clip_name;
    public float fall_speed;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        spider.direction.SetDirection(Random.Range(0, 2) == 0 ? 0 : 0.5f);
        spider.animator.Play(clip_name);
    }

    public override void Do()
    {
        spider.visuals.localPosition = Vector3.up * Mathf.Max(0, spider.visuals.localPosition.y - fall_speed * Time.deltaTime);
        if (spider.visuals.localPosition.y == 0)
            is_complete = true;
    }

    public override State Next()
    {
        return spider.land;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        spider = provider as Spider;
    }
}

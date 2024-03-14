using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRangeHug : State
{
    SpiderRange range;

    public string clip_name;
    public float push_speed;
    Vector2 direction_to_player;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;
        range.spider.animator.Play(clip_name);

        range.spider.action.SetNextState(range.spider.action.fight_spider);
        range.spider.action.fight_spider.catcher = range.spider;

        range.spider.visuals.localPosition = Vector3.zero;
        range.spider.transform.position = range.spider.target.position - Vector3.up * 0.25f;

        range.spider.rb.velocity = Vector2.zero;
        range.spider.col.enabled = false;

        range.did_catch = true;
    }

    public override void Do()
    {
    }

    public override State Next()
    {
        range.spider.col.enabled = false;
        range.spider.SetNextState(range.spider.fall);

        Vector3 push_direction = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-180, 180)) * Vector3.right;
        range.spider.rb.velocity = Utils.Warp(push_direction) * push_speed;

        float angle = Vector3.SignedAngle(Vector3.right, Vector2.Scale(push_direction, new Vector2(1, 2)), Vector3.forward);
        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;
        range.spider.direction.SetDirection(angle / 360f);

        range.spider.visuals.localPosition = Vector3.up * 1.25f;

        return null;
    }

    public override void Exit()
    {
        is_complete = false;
    }

    public override void Setup(MonoBehaviour provider)
    {
        range = provider as SpiderRange;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRangeHug : State
{
    SpiderRange range;

    public float push_speed;
    Vector2 direction_to_player;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        range.spider.action.SetNextState(range.spider.action.fight_spider);
        range.spider.action.fight_spider.catcher = range.spider;

        range.spider.visuals.localPosition = Vector3.up * 1.25f;
        range.spider.visuals.GetComponent<SpriteRenderer>().enabled = false;
        direction_to_player = range.spider.target.position - range.spider.transform.position;

        float angle = Vector3.SignedAngle(Vector3.right, Vector2.Scale(direction_to_player.normalized, new Vector2(1, 2)), Vector3.forward);
        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;
        range.spider.direction.SetDirection(angle / 360f);

        range.spider.rb.velocity = Vector2.zero;
    }

    public override void Do()
    {
    }

    public override State Next()
    {
        range.spider.SetNextState(range.spider.fall);
        range.spider.visuals.GetComponent<SpriteRenderer>().enabled = true;
        range.spider.rb.velocity = -direction_to_player.normalized * push_speed;
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

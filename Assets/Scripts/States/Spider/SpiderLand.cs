using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLand : State
{
    Spider spider;

    public string clip_name;
    public float time_stunned;

    bool first_land;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();

        spider.animator.Play(clip_name);
        is_complete = false;

        spider.origin_position = spider.transform.position;
    }

    public override void Do()
    {
        float speed = spider.rb.velocity.magnitude;
        if (speed > 0)
        {
            speed = Mathf.Max(0, speed - spider.range.jump.friction * Time.deltaTime);
            spider.rb.velocity = spider.rb.velocity.normalized * speed;
        }

        if (time >= time_stunned)
            is_complete = true;
    }

    public override State Next()
    {
        if (first_land)
        {
            first_land = false;
            return spider.passive;

        }
        else if (spider.range.did_catch)
        {
            return spider.escape;
        }
        return spider.chase;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        spider = provider as Spider;
    }
}

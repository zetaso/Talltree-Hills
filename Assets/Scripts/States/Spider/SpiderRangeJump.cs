using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRangeJump : State
{
    SpiderRange range;

    public string clip_name, land_clip_name;
    public float distance_forward, forward_multiplier, jump_height, jump_time, friction, catch_range;

    float flat_distance, x;
    Vector2 origin_point, destination_point, jump_direction;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        range.spider.animator.Play(clip_name);

        jump_direction = range.spider.target.position - range.spider.transform.position;
        if (jump_direction.magnitude > 8)
            jump_direction = jump_direction.normalized * 8f;

        origin_point = range.spider.transform.position;
        float dist_between = Vector2.Distance(range.spider.target.position, range.spider.transform.position);
        destination_point = origin_point + jump_direction.normalized * Mathf.Min(dist_between + distance_forward, dist_between * forward_multiplier);
        flat_distance = Vector2.Distance(origin_point, destination_point);

        float angle = Vector3.SignedAngle(Vector3.right, Vector2.Scale(jump_direction.normalized, new Vector2(1, 2)), Vector3.forward);
        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;
        range.spider.direction.SetDirection(angle / 360f);

        range.spider.rb.velocity = Utils.Warp(jump_direction.normalized * flat_distance) / jump_time;
    }

    public override void Do()
    {
        x = Vector2.Distance(origin_point, range.spider.transform.position) / flat_distance;

        if (x < 1)
        {
            range.spider.rb.velocity = Utils.Warp(jump_direction.normalized * flat_distance) / jump_time;
            float current_height = Mathf.Max(0, (-Mathf.Pow(2f * x - 1, 2) + 1) * jump_height);
            range.spider.visuals.localPosition = Vector3.up * current_height;

            if (Vector2.Scale(range.spider.target.position - range.spider.transform.position, new Vector2(1, 2)).magnitude <= catch_range && range.spider.action.state != range.spider.action.escape)
                range.SetNextState(range.hug);
        }
        else
        {
            range.spider.animator.Play(land_clip_name);

            range.spider.visuals.localPosition = Vector3.zero;

            float speed = range.spider.rb.velocity.magnitude;
            if (speed > 0)
            {
                speed = Mathf.Max(0, speed - friction * Time.deltaTime);
                range.spider.rb.velocity = range.spider.rb.velocity.normalized * speed;
            }

            if (speed == 0)
            {
                range.spider.SetNextState(range.spider.chase);
            }
        }
    }

    public override State Next()
    {
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

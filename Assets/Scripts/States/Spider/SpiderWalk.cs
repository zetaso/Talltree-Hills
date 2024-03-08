using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWalk : State
{
    SpiderPassive passive;

    public string clip_name;
    public float area_radius, min_walk_distance, speed;
    public Vector2 target_position;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = true;

        passive.spider.animator.Play(clip_name);

        target_position = passive.spider.origin_position + Random.insideUnitCircle * area_radius;
        if (Vector2.Distance(passive.spider.transform.position, target_position) < min_walk_distance)
            target_position = (Vector2)passive.spider.transform.position + Utils.Warp((target_position - (Vector2)passive.spider.transform.position).normalized * min_walk_distance);

        passive.spider.rb.velocity = Utils.Warp((target_position - (Vector2)passive.spider.transform.position).normalized) * speed;
    }

    public override void Do()
    {
        float angle = Vector3.SignedAngle(Vector3.right, Vector2.Scale(passive.spider.rb.velocity, new Vector2(1, 2)), Vector3.forward);
        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;
        passive.spider.direction.SetDirection(angle / 360f);
    }

    public override State Next()
    {
        if (Vector2.Distance(passive.spider.transform.position, target_position) < 0.1f)
            return passive.idle;
        return null;
    }

    public override void Exit()
    {
        is_complete = false;
        passive.spider.rb.velocity = Vector2.zero;
    }

    public override void Setup(MonoBehaviour provider)
    {
        passive = provider as SpiderPassive;
    }
}

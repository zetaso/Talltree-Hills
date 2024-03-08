using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderChase : State
{
    Spider spider;

    public string clip_name;
    public float min_chase_timeout, max_chase_timeout, speed;
    float chase_timeout;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = true;

        spider.animator.Play(clip_name);
        chase_timeout = Random.Range(min_chase_timeout, max_chase_timeout);
    }

    public override void Do()
    {
        spider.rb.velocity = Utils.Warp((spider.target.position - spider.transform.position).normalized) * speed;

        float angle = Vector3.SignedAngle(Vector3.right, Vector2.Scale(spider.rb.velocity, new Vector2(1, 2)), Vector3.forward);
        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;
        spider.direction.SetDirection(angle / 360f);
    }

    public override State Next()
    {
        if (Vector2.Scale(spider.target.position - spider.transform.position, new Vector2(1, 2)).magnitude <= spider.melee.attack_range)
            return spider.melee;
        else if (time >= chase_timeout)
            return spider.range;
        return null;
    }

    public override void Exit()
    {
        is_complete = false;
        spider.rb.velocity = Vector2.zero;
    }

    public override void Setup(MonoBehaviour provider)
    {
        spider = provider as Spider;
    }
}

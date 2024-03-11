using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderChase : State
{
    Spider spider;

    public string clip_name, idle_clip_name;
    public float min_chase_timeout, max_chase_timeout, speed, min_distance, max_distance, desaccel;
    float chase_timeout;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = true;

        spider.animator.Play(clip_name);
        spider.animator.speed = 1;
        chase_timeout = Random.Range(min_chase_timeout, max_chase_timeout);
    }

    public override void Do()
    {
        if (Vector2.Distance(spider.target.position, spider.transform.position) > max_distance)
        {
            spider.animator.Play(clip_name);
            spider.animator.speed = 1;
            spider.rb.velocity = Utils.Warp((spider.target.position - spider.transform.position).normalized) * speed;
        }
        else if (Vector2.Distance(spider.target.position, spider.transform.position) < min_distance)
        {
            spider.animator.Play(clip_name);
            spider.animator.speed = 1;
            spider.rb.velocity = Utils.Warp((spider.transform.position - spider.target.position).normalized) * speed;
        }
        else
        {
            spider.animator.Play(idle_clip_name, 0, 0);
            spider.animator.speed = 0;
            spider.rb.velocity = Vector2.zero;
        }

        float angle = Vector3.SignedAngle(Vector3.right, Vector2.Scale(spider.rb.velocity, new Vector2(1, 2)), Vector3.forward);
        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;
        spider.direction.SetDirection(angle / 360f);
    }

    public override State Next()
    {
        if (time >= chase_timeout)
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

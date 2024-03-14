using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderChase : State
{
    Spider spider;

    public string clip_name, idle_clip_name;
    public float min_chase_timeout, max_chase_timeout, speed, min_distance, max_distance, extra_distance;

    float chase_timeout;
    Vector3 destination;
    bool moving;
    float wait_time_left;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = true;

        spider.animator.Play(clip_name);
        spider.animator.speed = 1;
        chase_timeout = Random.Range(min_chase_timeout, max_chase_timeout);

        float current_distance = Vector2.Distance(spider.target.position, spider.transform.position);
        destination = spider.transform.position + (spider.target.position - spider.transform.position).normalized * (max_distance - current_distance + extra_distance);
    }

    public override void Do()
    {
        if (moving)
        {
            float current_distance = Vector2.Distance(destination, spider.transform.position);
            if (current_distance <= 0.1f)
            {
                moving = false;
                spider.animator.Play(idle_clip_name, 0, 0);
                spider.animator.speed = 0;
                spider.rb.velocity = Vector2.zero;
                wait_time_left = 0.25f;
            }
            else
            {
                spider.animator.Play(clip_name);
                spider.animator.speed = 1;
                spider.rb.velocity = Utils.Warp((destination - spider.transform.position).normalized) * speed;

                float angle = Vector3.SignedAngle(Vector3.right, Vector2.Scale(spider.rb.velocity, new Vector2(1, 2)), Vector3.forward);
                if (angle < 0)
                    angle += 360f;
                else if (angle > 360f)
                    angle -= 360f;
                spider.direction.SetDirection(angle / 360f);
            }
        }
        else if (wait_time_left <= 0)
        {
            float current_distance = Vector2.Distance(spider.target.position, spider.transform.position);
            if (current_distance > max_distance)
            {
                destination = spider.transform.position + (spider.target.position - spider.transform.position).normalized * (max_distance - current_distance + extra_distance);
                moving = true;
            }
            else if (current_distance < min_distance)
            {
                destination = spider.transform.position + (spider.transform.position - spider.target.position).normalized * (min_distance - current_distance + extra_distance);
                moving = true;
            }
        }
        else
        {
            wait_time_left -= Utils.unpausedDeltaTime;
        }
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
        spider.animator.speed = 1;
    }

    public override void Setup(MonoBehaviour provider)
    {
        spider = provider as Spider;
    }
}

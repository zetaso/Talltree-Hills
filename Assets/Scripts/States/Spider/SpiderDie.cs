using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpiderDie : State
{
    Spider spider;

    public string clip_name;
    public float time_to_dissapear;
    public UnityEvent on_die;

    public override void Trigger()
    {
        spider.SetNextState(this);
    }

    public override void Enter()
    {
        base.Enter();

        spider.animator.Play(clip_name);
        is_complete = false;
    }

    public override void Do()
    {
        float speed = spider.rb.velocity.magnitude;
        if (speed > 0)
        {
            speed = Mathf.Max(0, speed - spider.range.jump.friction * Time.deltaTime);
            spider.rb.velocity = spider.rb.velocity.normalized * speed;
        }

        spider.visuals.localPosition = Vector3.up * Mathf.Max(0, spider.visuals.localPosition.y - spider.fall.fall_speed * Time.deltaTime);

        if (time >= time_to_dissapear)
        {
            if (on_die != null)
                on_die.Invoke();
            Destroy(spider.gameObject);
        }
    }

    public override State Next()
    {
        return null;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        spider = provider as Spider;
    }
}

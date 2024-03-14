using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpiderEscape : State
{
    Spider spider;

    public string clip_name;
    public float speed;

    Vector3 destination;

    public override void Trigger()
    {
        spider.SetNextState(this);
    }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        spider.animator.Play(clip_name);
        spider.animator.speed = 1;

        destination = spider.transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-30, 30));
        spider.animator.speed = 1;
        spider.rb.velocity = Utils.Warp((destination - spider.transform.position).normalized) * speed * 2.5f;
        spider.col.enabled = false;

        float angle = Vector3.SignedAngle(Vector3.right, Vector2.Scale(spider.rb.velocity, new Vector2(1, 2)), Vector3.forward);
        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;
        spider.direction.SetDirection(angle / 360f);
    }

    public override void Do()
    {
        if (Vector2.Distance(spider.transform.position, destination) < 1f)
        {
            spider.SetNextState(spider.die);
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
        spider = provider as Spider;
    }
}

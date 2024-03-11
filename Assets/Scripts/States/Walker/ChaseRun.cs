using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseRun : State
{
    Chase chase;

    public string upper_clip_name, lower_clip_name;
    public float speed;

    Vector2 target_last_position;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = true;

        chase.walker.upper_animator.Play(upper_clip_name);
        chase.walker.lower_animator.Play(lower_clip_name);

        target_last_position = chase.walker.target.position;
        chase.walker.rb.velocity = Utils.Warp((target_last_position - (Vector2)chase.walker.transform.position).normalized * speed);

        float angle = Vector3.SignedAngle(Vector3.right, Vector2.Scale(chase.walker.rb.velocity, new Vector2(1, 2)), Vector3.forward);
        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;
        chase.walker.direction.SetDirection(angle / 360f);
    }

    public override void Do()
    {
        if (Vector2.Scale(chase.walker.target.position - transform.position, new Vector2(1, 2)).magnitude <= chase.vision_range)
            target_last_position = chase.walker.target.position;

        chase.walker.rb.velocity = (target_last_position - (Vector2)chase.walker.transform.position).normalized * speed;

        float angle = Vector3.SignedAngle(Vector3.right, Vector2.Scale(chase.walker.rb.velocity, new Vector2(1, 2)), Vector3.forward);
        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;
        chase.walker.direction.SetDirection(angle / 360f);

        string name = chase.walker.lower_renderer.sprite.name;
        string number = name.Substring(name.Length - 2, 2);
        if (number[0] == '_')
            number = number[1].ToString();
        int index = int.Parse(number);
        if (index % 2 == 1)
            chase.walker.upper_renderer.transform.localPosition = Vector3.zero;
        else if (index % 4 == 0)
            chase.walker.upper_renderer.transform.localPosition = Vector3.up * 0.125f;
        else
            chase.walker.upper_renderer.transform.localPosition = Vector3.down * 0.125f;
    }

    public override State Next()
    {
        if (Vector2.Scale(chase.walker.target.position - transform.position, new Vector2(1, 2)).magnitude <= chase.catch_.catch_range)
            return chase.catch_;
        else if (Vector2.Distance(chase.walker.transform.position, target_last_position) < 0.1f)
            return chase.idle;
        return null;
    }

    public override void Exit()
    {
        is_complete = false;
    }

    public override void Setup(MonoBehaviour provider)
    {
        chase = provider as Chase;
    }
}

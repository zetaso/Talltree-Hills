using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuietWalk : State
{
    Quiet quiet;

    public string upper_clip_name, lower_clip_name;
    public Transform[] positions;
    public float speed;

    int position_index;
    bool forward;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        quiet.walker.upper_animator.Play(upper_clip_name);
        quiet.walker.lower_animator.Play(lower_clip_name);

        position_index = Mathf.Clamp(position_index + (forward ? 1 : -1), 0, positions.Length - 1);

        if (position_index == positions.Length - 1)
            forward = false;
        else if (position_index == 0)
            forward = true;
    }

    public override void Do()
    {
        if (Vector2.Distance(quiet.walker.transform.position, positions[position_index].position) <= 0.1f)
            is_complete = true;

        quiet.walker.rb.velocity = Utils.Warp((positions[position_index].position - quiet.walker.transform.position).normalized * speed);

        float angle = Vector3.SignedAngle(Vector3.right, Vector2.Scale(quiet.walker.rb.velocity, new Vector2(1, 2)), Vector3.forward);
        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;
        quiet.walker.direction.SetDirection(angle / 360f);

        string name = quiet.walker.lower_renderer.sprite.name;
        string number = name.Substring(name.Length - 2, 2);
        if (number[0] == '_')
            number = number[1].ToString();
        int index = int.Parse(number);
        if (index % 2 == 1)
            quiet.walker.upper_renderer.transform.localPosition = Vector3.zero;
        else if (index % 4 == 0)
            quiet.walker.upper_renderer.transform.localPosition = Vector3.up * 0.125f;
        else
            quiet.walker.upper_renderer.transform.localPosition = Vector3.down * 0.125f;
    }

    public override State Next()
    {
        return quiet.idle;
    }

    public override void Exit()
    {
        is_complete = false;
    }

    public override void Setup(MonoBehaviour provider)
    {
        quiet = provider as Quiet;
    }
}

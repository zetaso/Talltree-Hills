using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAnticipate : State
{
    protected Flying flying;
    public string clip_name;
    public float clip_duration;

    public GameObject target_indicator_prefab;
    public GameObject target_indicator;
    public float forward_distance, random_radius;

    public override void Trigger()
    {
        flying.SetNextState(ref flying.state, this);
        is_complete = false;
    }

    public override void Enter()
    {
        base.Enter();

        if (target_indicator != null)
            Destroy(target_indicator);

        Vector2 target_postion;
        if (flying.player.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            target_postion = (Vector2)flying.player.position + flying.player.GetComponent<Rigidbody2D>().velocity.normalized * forward_distance + Random.insideUnitCircle * random_radius;
        else
            target_postion = (Vector2)flying.player.position;

        target_indicator = Instantiate(target_indicator_prefab, target_postion, Quaternion.identity);

        float angle = Vector3.SignedAngle(Vector3.right, (target_indicator.transform.position - flying.transform.position).normalized, Vector3.forward);
        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;
        flying.direction.SetDirection(angle / 360f);
    }

    public override void Do()
    {
        if (time >= clip_duration)
            is_complete = true;
    }

    public override State Next()
    {
        return flying.fly_attack;
    }

    public override void Exit() { is_complete = false; }

    public override void Setup(MonoBehaviour provider)
    {
        flying = provider as Flying;
    }
}

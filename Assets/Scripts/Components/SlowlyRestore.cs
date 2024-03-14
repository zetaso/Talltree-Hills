using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlowlyRestore : MonoBehaviour
{
    public float speed, time_to_wait;
    public UnityEvent on_end;

    bool ended_moving;
    float time_passed;

    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Utils.dialogDeltaTime);
        if (Vector3.Distance(transform.localPosition, Vector3.zero) < 0.1f)
        {
            ended_moving = true;
            time_passed = 0;
        }

        if (ended_moving)
        {
            time_passed += Utils.dialogDeltaTime;
            if (time_passed >= time_to_wait)
            {

                enabled = false;
                if (on_end != null)
                    on_end.Invoke();
            }
        }
    }
}

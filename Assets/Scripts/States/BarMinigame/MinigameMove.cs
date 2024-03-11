using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameMove : State
{
    BarMinigame minigame;

    public Animator space_button;
    public float speed;
    bool pressed;

    public override void Enter()
    {
        base.Enter();
        is_complete = false;
        pressed = false;
    }

    public override void Do()
    {
        minigame.cursor.transform.position += Vector3.right * speed * Utils.unpausedDeltaTime;

        if (Input.GetKey(KeyCode.Space))
            space_button.Play("pressed");
        else
            space_button.Play("not pressed");

        if (Input.GetKeyDown(KeyCode.Space) || minigame.cursor.transform.position.x >= minigame.bar.transform.position.x + minigame.bar.size.x * 0.5f)
        {
            pressed = true;
            is_complete = true;
        }
    }

    public override State Next()
    {
        if (minigame.cursor.transform.position.x >= minigame.area.transform.position.x - minigame.area.size.x * 0.5f &&
            minigame.cursor.transform.position.x <= minigame.area.transform.position.x + minigame.area.size.x * 0.5f && pressed)
            return minigame.success;
        return minigame.fail;
    }

    public override void Exit()
    {
        is_complete = false;
    }

    public override void Setup(MonoBehaviour provider)
    {
        minigame = provider as BarMinigame;
    }
}

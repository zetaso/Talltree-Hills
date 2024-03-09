using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : State
{
    public Action action { get; private set; }

    public string clip_name;

    public GameObject escape_minigame, prefab;
    public Transform prefab_parent;

    public override void Trigger() { }

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        action.animator.Play(clip_name);
        action.movement.GetComponent<SpriteRenderer>().enabled = false;

        escape_minigame = Instantiate(prefab, prefab_parent);
        escape_minigame.transform.localPosition = Vector2.zero;
        escape_minigame.transform.rotation = Quaternion.identity;
        escape_minigame.GetComponent<BarMinigame>().escape = this;

        action.movement.rb.velocity = Vector2.zero;
        Time.timeScale = 0;
    }

    public override void Do() { }

    public override State Next() { return null; }

    public override void Exit()
    {
        is_complete = false;
        action.movement.GetComponent<SpriteRenderer>().enabled = true;
        Time.timeScale = 1;
    }

    public override void Setup(MonoBehaviour provider)
    {
        action = provider as Action;
    }
}

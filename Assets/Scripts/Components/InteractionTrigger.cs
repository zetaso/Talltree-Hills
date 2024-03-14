using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Talker
{
    PLAYER,
    FRIEND,
    WALKER,
    SPIDER,
    FLYING,
    NOTE,
}

[System.Serializable]
public struct DialogMessage
{
    public Talker talker;
    public string message;
}

public class InteractionTrigger : MonoBehaviour
{
    public SpriteRenderer ren;
    public Transform indicator_position;
    public InteractionType interaction_type;
    public List<DialogMessage> origin_messages;
    public bool on_trigger_dialog;

    public UnityEvent onInteract, onEndInteract;

    public void OnInteracting()
    {
        if (onInteract != null)
            onInteract.Invoke();
    }
    public void OnEndInteracting()
    {
        if (onEndInteract != null)
            onEndInteract.Invoke();

        if (interaction_type == InteractionType.AMMO || interaction_type == InteractionType.HEAL)
        {
            Utils.Instance.interact.interaction_indicator.transform.parent = null;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Interact interact = other.gameObject.GetComponent<Interact>();
        if (interact && on_trigger_dialog)
            interact.ForceInteraction(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interact interact = other.GetComponent<Interact>();
        if (interact)
        {
            if (on_trigger_dialog)
                interact.ForceInteraction(this);
            else
                interact.AddTrigger(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Interact interact = other.GetComponent<Interact>();
        if (interact && !on_trigger_dialog)
            interact.RemoveTrigger(this);
    }

    public void ExecuteInteraction()
    {
        Utils.Instance.interact.ForceInteraction(this);
    }
}

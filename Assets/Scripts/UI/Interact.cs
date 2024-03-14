using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public List<InteractionTrigger> triggers;
    public DialogBox dialog_box;
    public InteractionIndicator interaction_indicator;
    public GameObject interaction_hint;

    public Action action;
    public Movement movement;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && triggers.Count > 0 && !Utils.Instance.pause.dialog && !Utils.Instance.pause.paused && !Utils.Instance.pause.dead)
        {
            dialog_box.EnterDialog(triggers[triggers.Count - 1]);
        }
    }

    public void ForceInteraction(InteractionTrigger trigger)
    {
        dialog_box.EnterDialog(trigger);
    }

    public void AddTrigger(InteractionTrigger trigger)
    {
        if (triggers.Count > 0)
            triggers[triggers.Count - 1].ren.material = Utils.Instance.default_material;

        triggers.Add(trigger);
        interaction_indicator.transform.parent = trigger.indicator_position;
        interaction_indicator.transform.localPosition = Vector3.zero;
        interaction_indicator.gameObject.SetActive(true);
        interaction_hint.SetActive(true);
        interaction_indicator.SetType(trigger.interaction_type);
        trigger.ren.material = Utils.Instance.outline_material;
    }

    public void RemoveTrigger(InteractionTrigger trigger)
    {
        triggers.Remove(trigger);
        trigger.ren.material = Utils.Instance.default_material;
        if (triggers.Count == 0)
        {
            interaction_indicator.gameObject.SetActive(false);
            interaction_hint.SetActive(false);

        }
        else if (triggers.Count > 0)
        {
            triggers[triggers.Count - 1].ren.material = Utils.Instance.outline_material;
            interaction_indicator.transform.parent = triggers[triggers.Count - 1].indicator_position;
            interaction_indicator.transform.localPosition = Vector3.zero;
            interaction_indicator.gameObject.SetActive(true);
            interaction_hint.SetActive(true);
            interaction_indicator.SetType(triggers[triggers.Count - 1].interaction_type);
        }
    }
}

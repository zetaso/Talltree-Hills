using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public DialogTrigger closest;
    public DialogBox dialog_box;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && closest != null)
        {
            dialog_box.EnterDialog(closest);
        }
    }

    public void TrySetTrigger(DialogTrigger new_trigger)
    {
        if (!closest || Vector2.Distance(new_trigger.transform.position, transform.position) < Vector2.Distance(closest.transform.position, transform.position))
        {
            if (closest)
                closest.ren.material = Utils.Instance.default_material;
            closest = new_trigger;
            closest.ren.material = Utils.Instance.outline_material;
        }
    }
}

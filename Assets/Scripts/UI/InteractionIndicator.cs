using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum InteractionType
{
    DIALOG,
    AMMO,
    HEAL,
}

[ExecuteInEditMode]
public class InteractionIndicator : MonoBehaviour
{
    public float max_distance, time_multiplier;

    public GameObject dialog, ammo;
    public TMP_Text hint;

    void Start()
    {
    }

    void Update()
    {
        transform.localPosition = Vector3.up * Mathf.Sin(Time.time * time_multiplier) * max_distance;
    }

    public void SetType(InteractionType interaction_type)
    {
        if (interaction_type == InteractionType.DIALOG)
        {
            dialog.SetActive(true);
            ammo.SetActive(false);
            hint.text = "Interact [E]";
        }
        else
        {
            dialog.SetActive(false);
            ammo.SetActive(true);
            hint.text = "Pick up [E]";
        }
    }
}

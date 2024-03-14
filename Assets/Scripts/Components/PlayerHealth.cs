using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float max_health, health, damage_rate;
    public bool damaging;

    public TMP_Text health_ui;

    void Update()
    {
        if (!Utils.Instance.pause.paused && !Utils.Instance.pause.dialog && !Utils.Instance.pause.cinematic && !Utils.Instance.pause.dead)
        {
            if (damaging)
            {
                health = Mathf.Max(0, health - damage_rate * Utils.unpausedDeltaTime);
                health_ui.text = Mathf.CeilToInt(health).ToString();
                if (health == 0)
                    Utils.Instance.pause.Die();
            }
        }
    }

    public void SetDamaging(bool b)
    {
        damaging = b;
    }

    public void Heal(int heal_value)
    {
        health = Mathf.Min(max_health, health + heal_value);
        health_ui.text = Mathf.CeilToInt(health).ToString();
    }
}

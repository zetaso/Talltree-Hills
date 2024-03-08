using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaUI : FloatingUI
{
    public Transform bar;

    public void SetStamina(float stamina)
    {
        bar.localScale = new Vector3(stamina, 1, 1);
    }
}

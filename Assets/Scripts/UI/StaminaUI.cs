using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaUI : FloatingUI
{
    public void SetStamina(float stamina)
    {
        sprites[1].transform.localScale = new Vector3(stamina, 1, 1);
    }
}

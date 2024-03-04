using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : FloatingUI
{
    public void SetAmmo(int ammo)
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            if (i < ammo)
                sprites[i].color = Utils.Instance.palette[(int)ColorTag.WHITE];
            else
                sprites[i].color = Utils.Instance.palette[(int)ColorTag.DARK_LILA];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampingData : SceneData
{
    public bool note_picked_up;
    public GameObject note;

    public override void InitScene()
    {
        if (note_picked_up)
            note.SetActive(false);
    }

    public void OnPickupNote()
    {
        note_picked_up = true;
    }
}

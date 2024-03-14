using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockerData : SceneData
{
    public bool backpack_picked_up;
    public GameObject backpack;

    public override void InitScene()
    {
        if (backpack_picked_up)
            backpack.SetActive(false);
    }

    public void OnPickupBackpack()
    {
        backpack_picked_up = true;
    }
}

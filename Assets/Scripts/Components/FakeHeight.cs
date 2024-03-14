using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeHeight : MonoBehaviour
{
    public Transform visuals;

    public void SetHeight(float height)
    {
        visuals.localPosition = Vector3.up * height;
    }
}

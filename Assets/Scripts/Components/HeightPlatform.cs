using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightPlatform : MonoBehaviour
{
    public float height;

    private void OnTriggerEnter2D(Collider2D other)
    {
        FakeHeight fake_height = other.GetComponent<FakeHeight>();
        if (fake_height)
            fake_height.SetHeight(height);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        FakeHeight fake_height = other.GetComponent<FakeHeight>();
        if (fake_height)
            fake_height.SetHeight(0);
    }
}

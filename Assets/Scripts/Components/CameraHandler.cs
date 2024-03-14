using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public float min_x, max_x; // local values
    public Transform player;

    void Update()
    {
        transform.position = Vector3.right * player.position.x;
        float local_x = transform.localPosition.x;
        transform.localPosition = Vector2.right * Mathf.Clamp(local_x, min_x, max_x);
    }
}

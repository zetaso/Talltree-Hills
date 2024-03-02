using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 velocity;

    PlayerState state;

    void Start()
    {

    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        velocity = Vector3.up * y * 0.62f + Vector3.right * x;

        transform.position += velocity * Time.deltaTime;

        if (velocity.sqrMagnitude > 0.01f)
        {
            state.TrySetState("moving", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector2 velocity_multiplier;
    public Vector3 velocity;

    PlayerState state;

    void Start()
    {
        state = GetComponent<PlayerState>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0 && y != 0)
        {
            x /= 1.4142f;
            y /= 1.4142f;
        }

        velocity = Vector3.up * y * velocity_multiplier.y + Vector3.right * x * velocity_multiplier.x;

        transform.position += velocity * Time.deltaTime;

        state.TrySetState("moving", velocity.sqrMagnitude > 0.01f);
    }
}

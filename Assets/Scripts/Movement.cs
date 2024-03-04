using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walking_speed, running_speed;
    public float sprint_duration, recover_duration;
    public float exhausted_time;
    public Vector2 velocity_multiplier;
    public Vector3 velocity;
    public StaminaUI staminaUI;

    float speed;
    float stamina;
    bool sprinting, recovering;
    PlayerState state;

    void Start()
    {
        state = GetComponent<PlayerState>();
        speed = walking_speed;
        stamina = 1;
        staminaUI.SetStamina(stamina);
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0 && y != 0)
        {
            x *= 0.7071f;
            y *= 0.7071f;
        }

        bool moving = x != 0 || y != 0;
        bool on_sprint = moving && !sprinting && !recovering && Input.GetKeyDown(KeyCode.LeftShift);

        state.TrySetState("moving", moving);

        if (on_sprint)
        {
            speed = running_speed;
            sprinting = true;
            state.TrySetState("running", sprinting);
        }

        if (sprinting)
        {
            stamina = Mathf.Max(0, stamina - Time.deltaTime / sprint_duration);
            if (stamina == 0)
            {
                sprinting = false;
                state.TrySetState("running", sprinting);
                speed = walking_speed;
                StartCoroutine(PlayerState.Delay(exhausted_time, () => { recovering = true; }));
            }

            staminaUI.SetStamina(stamina);
            staminaUI.KeepVisible();
        }

        if (recovering)
        {
            stamina = Mathf.Min(1, stamina + Time.deltaTime / recover_duration);
            if (stamina == 1)
                recovering = false;

            staminaUI.SetStamina(stamina);
            staminaUI.KeepVisible();
        }

        velocity = (Vector3.up * y * velocity_multiplier.y + Vector3.right * x * velocity_multiplier.x) * speed;
        transform.position += velocity * Time.deltaTime;
    }
}

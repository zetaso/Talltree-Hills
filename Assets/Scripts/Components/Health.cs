using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] spriteRenderers;
    public int max_health;
    public int health { get; private set; }
    public bool invincible;

    public UnityAction onDie;

    public float flash_time;
    float time_flashing;

    void Start()
    {
        health = max_health;
    }

    void Update()
    {
        if (time_flashing > 0)
        {
            time_flashing -= Time.deltaTime;
            if (time_flashing <= 0)
                SetFlashing(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (invincible)
            return;

        health = Mathf.Max(0, health - damage);
        SetFlashing(true);

        if (health == 0)
            onDie.Invoke();
    }

    public void SetFlashing(bool flashing)
    {
        if (!flashing)
        {
            foreach (var item in spriteRenderers)
                item.material = Utils.Instance.default_material;
        }
        else
        {
            time_flashing = flash_time;
            foreach (var item in spriteRenderers)
                item.material = Utils.Instance.white_material;
        }
    }

}

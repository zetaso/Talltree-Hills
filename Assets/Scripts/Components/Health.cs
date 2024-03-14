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

    public UnityAction onDie, onDamage;

    public float flash_time;
    float time_flashing;

    Material last_material;

    void Start()
    {
        health = max_health;
    }

    void Update()
    {
        if (time_flashing > 0)
        {
            time_flashing -= Time.unscaledDeltaTime;
            if (time_flashing <= 0)
                SetFlashing(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (invincible)
            return;

        health = Mathf.Max(0, health - damage);

        if (health == 0)
        {
            if (onDie != null)
                onDie.Invoke();
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (onDamage != null)
                onDamage.Invoke();
            SetFlashing(true);
        }
    }

    public void SetFlashing(bool flashing)
    {
        if (!flashing)
        {
            foreach (var item in spriteRenderers)
                item.material = last_material;
        }
        else
        {
            time_flashing = flash_time;
            if (spriteRenderers.Length >= 0)
                last_material = spriteRenderers[0].material;
            foreach (var item in spriteRenderers)
                item.material = Utils.Instance.white_material;
        }
    }

}

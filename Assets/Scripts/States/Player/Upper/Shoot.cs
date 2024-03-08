using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : State
{
    [SerializeField] Crosshair crosshair;
    [SerializeField] Recoil recoil;
    Action action;
    public string clip_name;

    public GameObject bullet_hit;
    public float bullet_hit_lifetime;

    public override void Enter()
    {
        base.Enter();
        is_complete = false;

        recoil.Trigger();

        action.animator.Play(clip_name);
        Transform flash = Instantiate(action.shoot_flash, transform.position, Quaternion.identity).transform;

        float angle = Vector3.SignedAngle(Vector3.right, crosshair.transform.position - Vector3.up * 1.625f - transform.position, Vector3.forward);

        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;

        action.direction.SetDirection(angle / 360f);
        int flash_index = (int)(action.direction.value * 8f + 0.5f);

        if (flash_index > 7)
            flash_index -= 8;
        else if (flash_index < 0)
            flash_index += 8;

        flash.GetChild(flash_index).gameObject.SetActive(true);
        Destroy(flash.gameObject, 0.125f);

        action.ammo--;
        action.ammoUI.SetAmmo(action.ammo);
        action.ammoUI.KeepVisible();

        Check();
    }

    public override void Do()
    {
        float angle = Vector3.SignedAngle(Vector3.right, crosshair.transform.position - Vector3.up * 1.625f - transform.position, Vector3.forward);

        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;

        action.direction.SetDirection(angle / 360f);

        if (time >= 0.5f)
            is_complete = true;
    }

    public override State Next()
    {
        if (Input.GetMouseButton(1))
            return action.aim;
        else
            return action.hold;
    }

    public override void Exit()
    {
        is_complete = false;
    }

    public override void Setup(MonoBehaviour provider)
    {
        action = provider as Action;
    }

    void Check()
    {
        float radius = (1 - crosshair.accuracy) * (crosshair.max_pixels - crosshair.min_pixels) / 8f;
        Vector2 hit_point = (Vector2)crosshair.transform.position + Random.insideUnitCircle * radius;

        Collider2D[] colliders = Physics2D.OverlapPointAll(hit_point);

        List<Health> healths = new List<Health>();

        foreach (var item in colliders)
        {
            Health health = item.transform.root.GetComponent<Health>();
            if (health)
                healths.Add(health);
        }

        SpriteRenderer bullet_sprite = Instantiate(bullet_hit, hit_point, Quaternion.identity).GetComponent<SpriteRenderer>();
        Destroy(bullet_sprite, bullet_hit_lifetime * 2f);
        FadeInOut bullet_fade = bullet_sprite.GetComponent<FadeInOut>();
        bullet_fade.start_alpha = 1;
        bullet_fade.end_alpha = 0;
        bullet_fade.time = bullet_hit_lifetime;
        bullet_fade.Restart();

        if (healths.Count > 0)
        {
            if (healths.Count > 1)
            {
                int in_front = 0;
                for (int i = 1; i < healths.Count; i++)
                {
                    if (healths[i].GetComponent<Sorting>().order > healths[in_front].GetComponent<Sorting>().order)
                        in_front = i;
                }
                healths[in_front].TakeDamage(1);
            }
            else
            {
                healths[0].TakeDamage(1);
            }

            bullet_sprite.color = Utils.Instance.palette[(int)ColorTag.WHITE];
        }
        else
        {
            bullet_sprite.color = Utils.Instance.palette[(int)ColorTag.LILA];
        }
    }
}

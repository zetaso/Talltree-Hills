using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shoot : State
{
    [SerializeField] Crosshair crosshair;
    [SerializeField] Recoil recoil;
    Action action;
    public string clip_name;

    public GameObject bullet_hit;
    public float bullet_hit_lifetime;

    public UnityEvent onShootEvent;

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

        action.OnShootAmmo();
        action.ammoUI.SetAmmo(action.ammo);
        action.ammoUI.KeepVisible();

        Check();

        if (onShootEvent != null)
            onShootEvent.Invoke();
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
        if (Input.GetMouseButton(1) && Utils.Instance.pause.CanInput())
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
        float radius = crosshair.transform.GetChild(0).GetChild(0).localPosition.x - 0.3125f;
        Vector2 hit_point = (Vector2)crosshair.transform.position + UnityEngine.Random.insideUnitCircle * radius;

        Collider2D[] colliders = Physics2D.OverlapPointAll(hit_point);

        List<Tuple<Health, int>> healths = new List<Tuple<Health, int>>();

        foreach (var item in colliders)
        {
            if (!item.isTrigger)
                continue;

            Transform current = item.transform;
            Health health = current.GetComponent<Health>();
            while (health == null && transform.parent != null)
            {
                current = current.parent;
                if (current)
                    health = current.GetComponent<Health>();
            }
            if (health)
                healths.Add(new Tuple<Health, int>(health, int.Parse(item.transform.name)));
        }

        SpriteRenderer bullet_sprite = Instantiate(bullet_hit, hit_point, Quaternion.identity).GetComponent<SpriteRenderer>();
        Destroy(bullet_sprite.gameObject, bullet_hit_lifetime * 2f);
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
                    if (healths[i].Item1.GetComponent<Sorting>().order > healths[in_front].Item1.GetComponent<Sorting>().order)
                        in_front = i;
                }
                healths[in_front].Item1.TakeDamage(1);
            }
            else
            {
                healths[0].Item1.TakeDamage(healths[0].Item2);
            }

            if (healths[0].Item1.invincible)
                bullet_sprite.color = Utils.Instance.palette[(int)ColorTag.LILA];
            else
                bullet_sprite.color = Utils.Instance.palette[(int)ColorTag.WHITE];
        }
        else
        {
            bullet_sprite.color = Utils.Instance.palette[(int)ColorTag.LILA];
        }
    }

    public void ClearCallback()
    {
        onShootEvent.RemoveAllListeners();
        onShootEvent = null;
    }
}

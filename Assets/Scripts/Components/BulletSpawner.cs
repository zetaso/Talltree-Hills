using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject[] bullet_prefabs;
    public float radius, time_between_bullets;
    public bool cut_y;

    float time_left;

    void Update()
    {
        time_left -= Time.deltaTime;
        if (time_left <= 0)
        {
            Vector2 relative_position = Random.insideUnitCircle * radius;
            Vector2 position = (Vector2)transform.position + (cut_y ? new Vector2(relative_position.x, relative_position.y * 0.707f) : relative_position);
            Instantiate(bullet_prefabs[Random.Range(0, bullet_prefabs.Length)], position, Quaternion.identity);
            time_left = time_between_bullets;
        }
    }
}

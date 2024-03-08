using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorting : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] spriteRenderers;

    public bool refresh;
    public int order;

    void Start()
    {
        Refresh();
    }

    void Update()
    {
        if (refresh)
            Refresh();
    }

    void Refresh()
    {
        order = (int)(-transform.position.y * 8f);
        foreach (var item in spriteRenderers)
        {
            item.sortingOrder = order;
        }
    }
}

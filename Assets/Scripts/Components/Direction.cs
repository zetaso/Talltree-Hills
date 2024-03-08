using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    [SerializeField] Animator[] animators;

    public float value;

    public void SetDirection(float v)
    {
        value = v;
        foreach (var item in animators)
        {
            item.SetFloat("direction", value);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focus : MonoBehaviour
{
    Transform target;

    void Update()
    {
        if (target)
            transform.position = target.position + Vector3.back * 10f;
    }

    public void SetNewPosition(Transform target)
    {
        this.target = target;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        transform.position = (Vector3)Input.mousePosition;
    }
}

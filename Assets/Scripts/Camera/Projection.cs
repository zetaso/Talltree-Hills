using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Projection : MonoBehaviour
{
    Camera cam;
    public float height = 1f;
    public float width = 1f;

    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cam == null)
            cam = GetComponent<Camera>();

        //stretch view//
        cam.ResetProjectionMatrix();
        var m = cam.projectionMatrix;

        m.m11 *= height;
        m.m00 *= width;
        cam.projectionMatrix = m;
    }
}

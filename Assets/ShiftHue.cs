using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftHue : MonoBehaviour
{
    Camera cam;

    public float shiftSpeed = 0.01f;
    float t = 0.0f;

    void Awake()
    {
        cam = GetComponent<Camera>(); 
    }

    void Update()
    {
        t += Time.deltaTime * shiftSpeed;
        if(t > 1.0f)
        {
            t = 0.0f;
        }
        cam.backgroundColor = Color.HSVToRGB(t, 0.7f, 0.7f);
    }
}

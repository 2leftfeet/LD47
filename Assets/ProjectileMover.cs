using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}

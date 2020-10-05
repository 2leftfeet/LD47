using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float speed;
    public GameObject hitEffect;

    void Awake()
    {
        Destroy(this.gameObject, 10.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }
}

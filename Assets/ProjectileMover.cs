using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float speed;
    public GameObject hitEffect;

    public AudioClip projectileDestroyAudio;
    [Range(0,1)]
    public float projectileDestroyAudioVolume;

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
        AudioManager.PlayClip(projectileDestroyAudio, projectileDestroyAudioVolume, 0.3f);
        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }
}

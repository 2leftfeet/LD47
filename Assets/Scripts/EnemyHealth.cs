using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Vector3 startPosition;

    Wander wander;
    Reload reload;

    void Start()
    {
        LoopManager.ResetReplay += ResetEnemy;
    }

    void OnDestroy()
    {
        LoopManager.ResetReplay -= ResetEnemy;
    }

    void Awake()
    {
        startPosition = transform.position;
        wander = GetComponent<Wander>();
        reload = GetComponent<Reload>();
    }

    public void ResetEnemy()
    {
        transform.position = startPosition;
        gameObject.SetActive(true);
        if(wander.enabled)
        {
            wander.Reset();
        }
        reload.Reset();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var projectile = other.GetComponent<ProjectileMover>();

        if(other)
        {
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}

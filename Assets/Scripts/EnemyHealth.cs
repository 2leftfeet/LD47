using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
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

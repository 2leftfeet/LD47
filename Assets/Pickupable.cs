using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    Vector3 startPosition;

    void Awake()
    {
        startPosition = transform.position;
        LoopManager.ResetReplay += ResetPickupable;
    }

    void OnDestroy()
    {
        LoopManager.ResetReplay -= ResetPickupable;
    }

    void ResetPickupable()
    {
        gameObject.SetActive(true);
        transform.position = startPosition;

        transform.parent = null;
        gameObject.layer = 9;
    }
}

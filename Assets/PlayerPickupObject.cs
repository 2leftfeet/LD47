using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupObject : MonoBehaviour
{
    public LayerMask pickupablesLayer;
    public float pickupRadius;
    public GameObject pickupRoot;

    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            var pickupable = Physics2D.OverlapCircle(transform.position, pickupRadius, pickupablesLayer);
            if(pickupable)
            {
                PickupObject(pickupable);
            }
        }
    }
    void PickupObject(Collider2D pickupable)
    {
        pickupable.transform.position = pickupRoot.transform.position;
        pickupable.gameObject.layer = 10;
        pickupable.transform.parent = pickupRoot.transform;
    }

    void DropObject()
    {

    }
}

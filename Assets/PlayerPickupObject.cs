using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupObject : MonoBehaviour
{
    public LayerMask pickupablesLayer;
    public float pickupRadius;
    public GameObject pickupRoot;
    public GameObject dropRoot;

    LoopTracker loopTracker;
    Collider2D pickedUp;
    [HideInInspector] public bool carrying = false;

    void Awake()
    {
        loopTracker = GetComponent<LoopTracker>();
    }

    void OnDisable()
    {
        DropObject();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(carrying)
            {
                DropAction da = new DropAction(this);
                loopTracker.RegisterAction(da);
                da.PlayAction();
            }
            else
            {
                PickupAction pa = new PickupAction(this);
                loopTracker.RegisterAction(pa);
                pa.PlayAction();
                
            }
        }
    }
    public void PickupObject()
    {
        var pickupable = Physics2D.OverlapCircle(transform.position, pickupRadius, pickupablesLayer);
        if(pickupable)
        {
            pickupable.transform.position = pickupRoot.transform.position;
            pickupable.gameObject.layer = 10;
            pickupable.transform.parent = pickupRoot.transform;
            pickedUp = pickupable;
            carrying = true;
        }
    }

    public void DropObject()
    {
        pickedUp.transform.position = dropRoot.transform.position;
        pickedUp.gameObject.layer = 9;
        pickedUp.transform.parent = null;
        pickedUp = null;
        carrying = false;
    }
}

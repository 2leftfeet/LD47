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
    CannonLookAtMouse cannon;
    Collider2D pickedUp;
    [HideInInspector] public bool carrying = false;
    [HideInInspector] public bool currentlyControlled = true;

    void Awake()
    {
        loopTracker = GetComponent<LoopTracker>();
        cannon = GetComponent<CannonLookAtMouse>();
    }

    void OnDisable()
    {
        DropObject();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && currentlyControlled)
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
            cannon.enabled = false;

            pickupable.transform.position = pickupRoot.transform.position;
            pickupable.gameObject.layer = 10;
            pickupable.transform.parent = pickupRoot.transform;
            pickedUp = pickupable;
            carrying = true;

            pickupable.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public void DropObject()
    {
        if(pickedUp)
        {
            cannon.enabled = true;

            pickedUp.transform.position = dropRoot.transform.position;
            pickedUp.gameObject.layer = 9;
            pickedUp.transform.parent = null;
            carrying = false;

            var rb = pickedUp.GetComponent<Rigidbody2D>();
            if(rb)
            {
                rb.isKinematic = false;
            }
            pickedUp = null;
        }
    }
}

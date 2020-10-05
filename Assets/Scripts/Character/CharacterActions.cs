using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalAction : InputAction
{
    public float movement;

    public HorizontalAction(float _movement, PhysicsController _player) : base(_player)
    {

        movement = Mathf.Clamp(_movement, -1f, 1f);
    }
    
    public override void PlayAction()
    {
        player.InputX = movement;
    }
}

public class JumpAction : InputAction
{
    public bool isDown;

    public JumpAction(PhysicsController _player, bool _isDown) : base(_player)
    {
        isDown = _isDown;
    }

    public override void PlayAction()
    {
        player.JumpPressed = isDown;
    }
}

public class ShootAction: InputAction
{
    public Vector3 mousePosWorld;
    private CannonLookAtMouse cannonScript;

    public ShootAction(CannonLookAtMouse _cannon, Vector3 _mousePosWorld)
    {
        cannonScript = _cannon;
        mousePosWorld = _mousePosWorld;
    }

    public override void PlayAction()
    {
        cannonScript.ShootAtPoint(mousePosWorld);
    }
}

public class PickupAction : InputAction
{
    private PlayerPickupObject pickupObject;
    private Collider2D pickupable;

    public PickupAction(PlayerPickupObject _pickupObject){
        pickupObject = _pickupObject;
    }

    public override void PlayAction()
    {
        pickupObject.PickupObject();
    }
}

public class DropAction : InputAction
{
    private PlayerPickupObject pickupObject;

    public DropAction(PlayerPickupObject _pickupObject)
    {
        pickupObject = _pickupObject;
    }
    public override void PlayAction()
    {
        pickupObject.DropObject();
    }
}

public class KysAction : InputAction
{
    private CharController cc;

    public KysAction(CharController _cc)
    {
        cc = _cc;
    }

    public override void PlayAction()
    {
        cc.TriggerDeath(true);
    }
}
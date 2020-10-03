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

public class KysAction : InputAction
{
    private CharController cc;

    public KysAction(CharController _cc)
    {
        cc = _cc;
    }

    public override void PlayAction()
    {
        cc.TriggerDeath();
    }
}
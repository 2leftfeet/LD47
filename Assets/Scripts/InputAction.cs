using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAction
{
    public InputAction()
    {
        
    }
    
    public InputAction(PhysicsController _player)
    {
        player = _player;
    }

    protected PhysicsController player;
    public virtual void PlayAction(){}

}
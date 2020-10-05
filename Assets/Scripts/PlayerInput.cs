using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PhysicsController player;
    private CharController cc;
    private LoopTracker loopTracker;
    private CannonLookAtMouse cannon;

    float previousX = 0.0f;
    float currentX = 0.0f;

    bool createHorizontal = false;
    
    bool createJump = false;
    private bool isJumpKeyDown = false;

    private bool createKys = false;

    void Awake()
    {
        player = GetComponent<PhysicsController>();
        cc = GetComponent<CharController>();
        loopTracker = GetComponent<LoopTracker>();
        cannon = GetComponent<CannonLookAtMouse>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpKeyDown = true;
            createJump = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumpKeyDown = false;
            createJump = true;
        }

        // TODO: To cleanup if it works
        if(Input.GetKeyDown(KeyCode.Return))
        {
            createKys = true;
        }
    }

    public void Reset()
    {
        createKys = false;
        createJump = false;
        createHorizontal = false;
    }
    
    void FixedUpdate()
    {
        currentX = Input.GetAxisRaw("Horizontal");
        if(Mathf.Abs(previousX - currentX) > 0.0f)
        {
            createHorizontal = true;   
        }
        previousX = currentX;

        if (createKys)
        {
            var action = new KysAction(cc);
            loopTracker.RegisterAction(action);
            action.PlayAction();
            createKys = false;
        }
        if(createHorizontal)
        {
            var action = new HorizontalAction(previousX, player);
            loopTracker.RegisterAction(action);
            action.PlayAction();
            createHorizontal = false;
        }

        if (createJump)
        {
            var action = new JumpAction(player, isJumpKeyDown);
            loopTracker.RegisterAction(action);
            action.PlayAction();
            createJump = false;
        }
    }

    public void StopMovement()
    {
        
        var action = new HorizontalAction(0.0f, player);
        loopTracker.RegisterAction(action);
        action.PlayAction();

         var action2 = new JumpAction(player, false);
        loopTracker.RegisterAction(action2);
        action2.PlayAction();
         
    }
}

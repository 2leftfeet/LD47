using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PhysicsController player;
    private CharController cc;
    private LoopTracker loopTracker;

    float previousX = 0.0f;

    bool createHorizontal = false;
    
    bool createJump = false;
    private bool isJumpKeyDown = false;

    private bool createKys = false;

    void Awake()
    {
        player = GetComponent<PhysicsController>();
        cc = GetComponent<CharController>();
        loopTracker = GetComponent<LoopTracker>();
    }

    void Update()
    {
        float currentX = Input.GetAxisRaw("Horizontal");
        if(Mathf.Abs(previousX - currentX) > 0.0f)
        {
            createHorizontal = true;   
        }

        previousX = currentX;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpKeyDown = true;
            createJump = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumpKeyDown = false;
            createJump = true;
        }

        // TODO: To cleanup if it works
        createKys = Input.GetKeyDown(KeyCode.Return);
        if (createKys)
        {
            var action = new KysAction(cc);
            loopTracker.RegisterAction(action);
            action.PlayAction();
            createKys = false;
        }
    }

    void FixedUpdate()
    {
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
}

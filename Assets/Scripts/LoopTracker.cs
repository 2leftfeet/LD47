using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopTracker : MonoBehaviour
{
    struct ActionEntry
    {
        public InputAction InputAction;
        public int CapturedFrame;
    }

    struct MouseEntry
    {
        public Vector3 MousePos;
        public int CapturedFrame;
    }

    // Thought to use Queue here but we need to retain the actions between loops
    private List<ActionEntry> actionEntries = new List<ActionEntry>(1024);
    private List<MouseEntry> mouseEntries = new List<MouseEntry>(1024);

    private Vector3 startingPos;
    
    private bool isPlayerControlled = true;
    public bool IsPlayerControlled => isPlayerControlled;
    
    private bool isReplaying = false;
    public int currFixedFrame = 0;
    public int currIndex = 0;
    public int currMouseIndex = 0;

    private CannonLookAtMouse cannon;
    private PhysicsController physController;

    private void Awake()
    {
        startingPos = transform.position;
        
        LoopManager.StartReplay += StartReplay;
        LoopManager.ResetReplay += Reset;

        cannon = GetComponent<CannonLookAtMouse>();
        physController = GetComponent<PhysicsController>();
    }

    private void FixedUpdate()
    {
        // Actions
        if (isReplaying && actionEntries.Count > currIndex)
        {
            ActionEntry ae = actionEntries[currIndex];
            while (ae.CapturedFrame <= currFixedFrame)
            {
                ae.InputAction.PlayAction();
                currIndex++;
                
                // Assign new current ae or break out 
                if(actionEntries.Count > currIndex)
                    ae = actionEntries[currIndex];
                else
                    break;
                
            }
        }

        // Mouse Entries
        if(isReplaying && mouseEntries.Count > currMouseIndex + 1)
        {
            MouseEntry me = mouseEntries[currMouseIndex];
            MouseEntry meNext = mouseEntries[currMouseIndex + 1];
            cannon.LookAtPoint(Vector3.Lerp(me.MousePos, meNext.MousePos, (float)(currFixedFrame - me.CapturedFrame) / (meNext.CapturedFrame - me.CapturedFrame)));
            //Debug.Log($"Lerping from mousepos index {currMouseIndex} to {currMouseIndex + 1}. t = {(float)(currFixedFrame - me.CapturedFrame) / meNext.CapturedFrame - me.CapturedFrame}");
            //Debug.Log($"Current frame {currFixedFrame}, index frame{me.CapturedFrame}, index + 1 frame {meNext.CapturedFrame}");
            if(currFixedFrame > meNext.CapturedFrame)
            {
                currMouseIndex++;
            }
        }
        
        // Do this AT THE END of the frame
        currFixedFrame++;
    }

    public void RegisterAction(InputAction inputAction)
    {
        actionEntries.Add(new ActionEntry
        {
            InputAction = inputAction,
            CapturedFrame = currFixedFrame
        });
    }

    public void RegisterMousePos(Vector3 mousePos)
    {
        mouseEntries.Add(new MouseEntry
        {
            MousePos = mousePos,
            CapturedFrame = currFixedFrame
        });
    }

    public void StartReplay()
    {   
        MarkAsPlayerControlled(false);
        isReplaying = true;
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        physController.Reset();
        
        // Add vector forward to move dead 
        transform.position = startingPos + Vector3.forward;
        isReplaying = false;
        currFixedFrame = 0;
        currIndex = 0;
        currMouseIndex = 0;
    }

    public void MarkAsPlayerControlled(bool value)
    {
        isPlayerControlled = value;
        // TODO: move somewhere else mby
        GetComponent<PlayerInput>().enabled = value;
        GetComponent<CannonLookAtMouse>().enabled = value;
    }

    private void OnDestroy()
    {
        LoopManager.StartReplay -= StartReplay;
        LoopManager.ResetReplay -= Reset;
    }
}

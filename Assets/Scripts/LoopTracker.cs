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

    // Thought to use Queue here but we need to retain the actions between loops
    private List<ActionEntry> actionEntries = new List<ActionEntry>(1024);
        
    private bool isReplaying = false;
    public int currFixedFrame = 0;
    public int currIndex = 0;

    private void FixedUpdate()
    {
        if (isReplaying)
        {
            ActionEntry ae = actionEntries[currIndex];
            while (ae.CapturedFrame >= currFixedFrame)
            {
                ae.InputAction.PlayAction(); 
                currIndex++;
                
                // Assign new current ae
                ae = actionEntries[currIndex];
            }
            
            // Do this AT THE END of the frame
            currFixedFrame++;
        }
    }

    public void RegisterAction(InputAction inputAction)
    {
        actionEntries.Add(new ActionEntry
        {
            InputAction = inputAction,
            CapturedFrame = currFixedFrame
        });
    }

    public void StartReplay()
    {
        isReplaying = true;
    }

    public void Reset()
    {
        isReplaying = false;
        currFixedFrame = 0;
        currIndex = 0;
    }

    private void OnEnable()
    {
        LoopManager.StartReplay += StartReplay;
        LoopManager.ResetReplay += Reset;
    }
    
    private void OnDisable()
    {
        LoopManager.StartReplay -= StartReplay;
        LoopManager.ResetReplay -= Reset;
    }
}

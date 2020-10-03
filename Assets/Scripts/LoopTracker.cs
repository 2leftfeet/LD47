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

    private Vector3 startingPos;
    
    private bool isPlayerControlled = true;
    public bool IsPlayerControlled => isPlayerControlled;
    
    private bool isReplaying = false;
    public int currFixedFrame = 0;
    public int currIndex = 0;

    private void Awake()
    {
        startingPos = transform.position;
        
        LoopManager.StartReplay += StartReplay;
        LoopManager.ResetReplay += Reset;
    }

    private void FixedUpdate()
    {
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

    public void StartReplay()
    {   
        MarkAsPlayerControlled(false);
        isReplaying = true;
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        
        transform.position = startingPos;
        isReplaying = false;
        currFixedFrame = 0;
        currIndex = 0;
    }

    public void MarkAsPlayerControlled(bool value)
    {
        isPlayerControlled = value;
        // TODO: move somewhere else mby
        GetComponent<PlayerInput>().enabled = value;
    }

    private void OnDestroy()
    {
        LoopManager.StartReplay -= StartReplay;
        LoopManager.ResetReplay -= Reset;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopTracker : MonoBehaviour
{
    struct ActionEntry
    {
        public Action action;
        public uint frame;
    }

    // Thought to use Queue here but we need to retain the actions between loops
    private List<ActionEntry> actionEntries = new List<ActionEntry>(1024);
        
    private bool isReplaying = false;
    public uint currFixedFrame = 0;
    public uint currIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        
        
        // Do this AT THE END of the frame
        currFixedFrame++;
    }

    public void RegisterAction(Action action)
    {
        
    }

    public void StartReplay()
    {
        isReplaying = true;
    }

    public void Reset()
    {
        currFixedFrame = 0;
        currIndex = 0;
    }
}

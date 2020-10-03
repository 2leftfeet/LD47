using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public static LoopManager Instance => instance;
    private static LoopManager instance;

    public static event Action StartReplay;
    public static event Action ResetReplay;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError($"This is not okie dokie, two {GetType().ToString()} exist");
        }
    }

    public void StartReplays()
    {
        StartReplay?.Invoke();
    }

    public void ResetReplays()
    {
        ResetReplay?.Invoke();
    }
}

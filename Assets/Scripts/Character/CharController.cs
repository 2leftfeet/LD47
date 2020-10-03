using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private LoopTracker loopTracker;
    private bool IsPlayerControlled => loopTracker.IsPlayerControlled;

    private void Awake()
    {
        loopTracker = GetComponent<LoopTracker>();
    }

    public void TriggerDeath()
    {
        gameObject.SetActive(false);
        if (IsPlayerControlled)
        {
            LevelManager.Instance.TriggerDeath();
        }
    }
}

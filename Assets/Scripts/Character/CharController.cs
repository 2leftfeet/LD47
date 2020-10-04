using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField]
    private Material deathMaterial = null;
    private SpriteRenderer[] spriteRenderers;
    
    private LoopTracker loopTracker;
    private bool IsPlayerControlled => loopTracker.IsPlayerControlled;

    private void Awake()
    {
        loopTracker = GetComponent<LoopTracker>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void TriggerDeath()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sharedMaterial = deathMaterial;
        }
        
        if (IsPlayerControlled)
        {
            LevelManager.Instance.TriggerDeath();
        }
    }
}

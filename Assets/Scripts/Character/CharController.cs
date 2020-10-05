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
    private Animator animator;
    private Collider2D collider;
    private PlayerPickupObject pickupObject;
    private PlayerInput input;
    private CannonLookAtMouse cannon;

    private void Awake()
    {
        loopTracker = GetComponent<LoopTracker>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        pickupObject = GetComponent<PlayerPickupObject>();
        input = GetComponent<PlayerInput>();
        cannon = GetComponent<CannonLookAtMouse>();

        LoopManager.ResetReplay += ResetPlayer;
    }

    public void OnDestroy()
    {
        LoopManager.ResetReplay -= ResetPlayer;
    }

    public void TriggerDeath()
    {
        animator.SetTrigger("Die");
        animator.gameObject.tag = "DeadPlayer";

        pickupObject.DropObject();
        input.StopMovement();
        input.enabled = false;
        cannon.enabled = false;

        
        if (IsPlayerControlled)
        {
            LevelManager.Instance.TriggerDeath();
        }
    }

    public void ResetPlayer()
    {
        animator.gameObject.tag  = "Player";

        animator.SetTrigger("Reset");
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sharedMaterial = deathMaterial;
        }
    }
}

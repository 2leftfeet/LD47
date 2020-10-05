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

    
    [Header("Audio")]
    public List<AudioClip> deathAudio = new List<AudioClip>();
    public AudioClip cloneDeathAudio;
    [Range(0,1)]
    public float audioVolume;

    public AudioClip respawnAudio;
    [Range(0,1)]
    public float respawnAudioVolume;
    public AudioClip kysAudio;
    [Range(0,1)]
    public float kysAudioVolume;
    AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
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

    public void TriggerDeath(bool isKys = false)
    {
        source.volume = 0.1f;

        animator.SetTrigger("Die");
        animator.gameObject.tag = "DeadPlayer";

        pickupObject.DropObject();
        input.StopMovement();
        input.enabled = false;
        cannon.enabled = false;

        
        if (IsPlayerControlled)
        {
            if(isKys){
                AudioManager.PlayClip(kysAudio,kysAudioVolume);
            }
            else{
                AudioManager.PlayClip(deathAudio[UnityEngine.Random.Range(0,deathAudio.Count-1) ],audioVolume);
            }
            AudioManager.PlayClip(respawnAudio,respawnAudioVolume);
            LevelManager.Instance.TriggerDeath();
        }
        else{
            AudioManager.PlayClip(cloneDeathAudio,audioVolume,0.3f);
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

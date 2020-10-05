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
    }

    public void TriggerDeath(bool isKys = false)
    {
        source.volume = 0.1f;
        gameObject.SetActive(false);
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sharedMaterial = deathMaterial;
        }
        
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
}

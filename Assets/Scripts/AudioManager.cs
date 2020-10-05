using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AudioManager : SingletonBehavior<AudioManager>
{
    private AudioSource audioSource;

    void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public static void PlayClip(AudioClip clip, float volume = 1, float pitchRange = 0)
    {
        Instance.audioSource.pitch = UnityEngine.Random.Range(1 - pitchRange, 1 + pitchRange);
        Instance.audioSource.PlayOneShot(clip,volume);
    }

    public static void PlayMusic()
    {
        throw new NotImplementedException();
    }
    
    public enum MusicType { MainTheme, Scene, Credits }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static void PlayClip(AudioClip clip,float volume=1)
    {
        Instance.audioSource.PlayOneShot(clip,volume);
    }

    public static void PlayMusic()
    {
        throw new NotImplementedException();
    }
    
    public enum MusicType { MainTheme, Scene, Credits }
}

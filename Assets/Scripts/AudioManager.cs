using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonBehavior<AudioManager>
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public static void PlayClip(AudioClip clip)
    {
        Instance.audioSource.PlayOneShot(clip);
    }

    public static void PlayMusic()
    {
        throw new NotImplementedException();
    }
    
    public enum MusicType { MainTheme, Scene, Credits }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonBehavior<AudioManager>
{
    public static AudioManager Instance => instance;
    private static AudioManager instance;

    private AudioSource audioSource;

    public static void PlayClip(AudioClip clip)
    {
        instance.audioSource.PlayOneShot(clip);
    }

    public static void PlayMusic()
    {
        throw new NotImplementedException();
    }
    
    public enum MusicType { MainTheme, Scene, Credits }
}

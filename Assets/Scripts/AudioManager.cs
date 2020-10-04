using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance => instance;
    private static AudioManager instance;

    private AudioSource audioSource;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogError($"This is not okie dokie, two {GetType().ToString()} exist");
        }
        
        DontDestroyOnLoad(gameObject);
    }

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

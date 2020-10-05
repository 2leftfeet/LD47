using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineLoop : MonoBehaviour
{
    public AudioClip engine;

    [Range(0,1)]
    public float engineAudioVolume;

    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable(){
        source = GetComponent<AudioSource>();
        source.loop = true;
        source.clip = engine;
        source.volume = engineAudioVolume;
        source.Play();

    }
}

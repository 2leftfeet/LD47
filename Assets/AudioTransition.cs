using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTransition : MonoBehaviour
{
    public AudioClip explosion;

    [Range(0,1)]
    public float explosionAudioVolume;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode(){
        AudioManager.PlayClip(explosion,explosionAudioVolume);
    }
    void OnEnable(){
        Explode();
    }
}

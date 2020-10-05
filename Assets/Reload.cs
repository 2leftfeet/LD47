using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{

    public FieldOfView fov;
    Animator animator;
    public float reloadTime = 3f;
    float reloadTimer = 0.0f;
    IEnumerator coroutine;

    AudioSource source;

    public AudioClip reloadAudio;
    [Range(0,1)]
    public float reloadAudioAudioVolume;
    AudioClip runningAudio;
 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        runningAudio = source.clip;
    }

    public void ReloadQueue(){
        source.clip = reloadAudio;
        source.volume = reloadAudioAudioVolume;
        fov.enabled = false;
        animator.SetBool("Reloading", true);
        reloadTimer = 0.0f;
    }

    public void Reset()
    {
        source.clip = runningAudio;
        source.volume = 1f;
        reloadTimer = 0.0f;
        fov.enabled = true;
        animator.SetBool("Reloading", false);
    }
    // Update is called once per frame
    void Update()
    {
        if(fov.enabled == false && reloadTimer > reloadTime)
        {
            source.clip = runningAudio;
            source.volume = 1f;
            fov.enabled = true;
            animator.SetBool("Reloading", false);
            reloadTimer = 0.0f;
        }
        reloadTimer += Time.deltaTime;
    }
}

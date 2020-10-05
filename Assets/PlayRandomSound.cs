using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{

    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.PlayClip(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

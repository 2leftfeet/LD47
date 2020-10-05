using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class resetAnimation : MonoBehaviour
{
    PlayableDirector playableDirector;

    private void OnDisable()
    {
        
    }

    private void OnEnable()
    {
        LoopManager.ResetReplay += Reset;
    }
    // Start is called before the first frame update
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    void Reset()
    {
        if(playableDirector)
            playableDirector.time = 0;
    }
}

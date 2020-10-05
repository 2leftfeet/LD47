using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(OverlapTrigger))]
public class LevelEndSequence : MonoBehaviour
{
    OverlapTrigger trigger;
    GameObject winnerScreen;

    bool waitingStarted;
    bool skipped;
    bool levelChanged = false;

    public AudioClip yeahAudio;
    [Range(0,1)]
    public float yeahAudioVolume;
    public AudioClip transitionAudio;
    [Range(0,1)]
    public float transitionAudioVolume;

    //something audio reference

    GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        // This is my magnum bopis, if anyone sees this script, tell them Arnas made this
        // Simplier setup because you no need to asign shit. :) Only downside -50fps
        parent = GameObject.Find("Intro");
        foreach(Transform child in parent.transform)
        {
        if(child.tag == "EndScreen")
            winnerScreen = child.gameObject;
        }
        // magnum bopis pabaiga

        waitingStarted = false;
        skipped = false;
        trigger = GetComponent<OverlapTrigger>();
        trigger.OnTriggerEnter += EndLevel;
    }


    private void Update()
    {
    }


    void EndLevel(Collider2D other)
    {
        AudioManager.PlayClip(yeahAudio,yeahAudioVolume);
        AudioManager.PlayClip(transitionAudio,transitionAudioVolume);
        //Time.fixedDeltaTime = 0f;
        winnerScreen.SetActive(true);
        if (!levelChanged)
        {
            levelChanged = true;
            StartCoroutine("waitingToLoad");
        }
    }
    

    IEnumerator waitingToLoad()
    {

        yield return new WaitForSecondsRealtime(3f);
        if (!skipped)
        {
            //Time.fixedDeltaTime = 0.02f;
            //winnerScreen.SetActive(false);
            LevelManager.Instance.TriggerVictory();
        }
    }
}

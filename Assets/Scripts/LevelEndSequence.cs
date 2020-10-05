using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(OverlapTrigger))]
public class LevelEndSequence : MonoBehaviour
{
    OverlapTrigger trigger;

    [SerializeField]
    GameObject winnerScreen;

    bool waitingStarted;
    bool skipped;

    //something audio reference

    

    // Start is called before the first frame update
    void Start()
    {
        waitingStarted = false;
        skipped = false;
        trigger = GetComponent<OverlapTrigger>();
        trigger.OnTriggerEnter += EndLevel;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && waitingStarted)
        {
            StopCoroutine("waitingToLoad");
            skipped = true;
            //winnerScreen.SetActive(false);
            //Time.fixedDeltaTime = 0.02f;
            LevelManager.Instance.TriggerVictory();
        }
    }


    void EndLevel(Collider2D other)
    {
        //Time.fixedDeltaTime = 0f;
        winnerScreen.SetActive(true);
        StartCoroutine("waitingToLoad");
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

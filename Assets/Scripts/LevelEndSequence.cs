using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(OverlapTrigger))]
public class LevelEndSequence : MonoBehaviour
{
    OverlapTrigger trigger;

    GameObject winnerScreen;

    bool lastLevel;

   //something audio reference

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<OverlapTrigger>();
        trigger.OnTriggerEnter += EndLevel;
    }



    void EndLevel(Collider2D other)
    {

    }
}

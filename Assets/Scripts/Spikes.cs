using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OverlapTrigger))]
public class Spikes : MonoBehaviour
{

    OverlapTrigger deathZone;


    private void Awake()
    {
        deathZone.OnTriggerEnter += KillPlayer;
    }

    void KillPlayer(Collider2D player)
    {
        //call a kill on a necesery player
        GetComponent<CharController>().TriggerDeath();
    }
}

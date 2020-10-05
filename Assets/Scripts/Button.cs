using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(OverlapTrigger))]
public class Button : MonoBehaviour
{

    public event System.Action OnActivate = delegate { };
    public event System.Action OnDeactivate = delegate { };

    OverlapTrigger trigger;

    // number of players required to activate
    [SerializeField] int numOfPlayerReq = 0;

    private int playersOnButton = 0;

    SpriteRenderer sprite;

    [Header("Sprites")]
    [SerializeField] Transform movingPart;
    [SerializeField] float moveAmount = 0.2f;
    
    [Header("Audio Clips")]
    [SerializeField] AudioClip onActivate;
    [SerializeField] AudioClip onDeactivate;

    bool gotActivated = false;

    private void Awake()
    {
        trigger = GetComponent<OverlapTrigger>();
        sprite = GetComponent<SpriteRenderer>();
        trigger.OnTriggerEnter += Enter;
        trigger.OnTriggerExit += Exit;
    }
        
    void Enter(Collider2D other)
    {
        playersOnButton++;
        Debug.Log(playersOnButton);
        if (playersOnButton >= numOfPlayerReq)
        {
            if (movingPart)
                movingPart.Translate(new Vector3(0f, -moveAmount, 0f));
            if (onActivate)
            {
                AudioManager.PlayClip(onActivate, 1f);
            }
            Debug.Log("GOT activated");
            if(!gotActivated)
                OnActivate();
            gotActivated = true;
        }
    }

    private void Exit(Collider2D other)
    {
        Debug.Log("Player exited");
        playersOnButton--;
        if (playersOnButton < numOfPlayerReq)
        {
            if(movingPart)
                movingPart.Translate(new Vector3(0f, moveAmount, 0f));
            if (onDeactivate)
            {
                AudioManager.PlayClip(onDeactivate, 0.5f);
            }
            if(gotActivated)
                OnDeactivate();
            gotActivated = false;
        }

    }
}

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
    
    AudioSource audio;

    private void Awake()
    {
        trigger = GetComponent<OverlapTrigger>();
        sprite = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        trigger.OnTriggerEnter += Enter;
        trigger.OnTriggerExit += Exit;
    }
        
    void Enter(Collider2D other)
    {
        playersOnButton++;
        if (playersOnButton == numOfPlayerReq)
        {
            if (movingPart)
                movingPart.Translate(new Vector3(0f, -moveAmount, 0f));
            if (onActivate)
            {
                audio.clip = onActivate;
                audio.Play();
            }  
            OnActivate();
        }
    }

    private void Exit(Collider2D other)
    {
        playersOnButton--;
        if (playersOnButton < numOfPlayerReq)
        {
            if(movingPart)
                movingPart.Translate(new Vector3(0f, moveAmount, 0f));
            if (onDeactivate)
            {
                audio.clip = onDeactivate;
                audio.Play();
            }
            OnDeactivate();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ButtonLisener : MonoBehaviour
{

    public event System.Action OnActivate = delegate { };
    public event System.Action OnDeactivate = delegate { };

    [SerializeField]
    OverlapTrigger trigger;

    private int playersOnButton = 0;

    [SerializeField] Color inactive;
    [SerializeField] Color active;

    [SerializeField] MeshRenderer mesh;

    private void Awake()
    {
        trigger.OnTriggerEnter += Enter;
        trigger.OnTriggerExit += Exit;
    }
        
    void Enter()
    {
        if (playersOnButton == 0)
        {
            mesh.material.SetColor("_BaseColor", active);
            mesh.material.SetColor("_EmissionColor", active);
            transform.Translate(new Vector3(0f, -0.05f, 0f));
            OnActivate();
        }

        playersOnButton++;
        
    }

    private void Exit()
    {
        
        if (playersOnButton == 0)
        {
            mesh.material.SetColor("_BaseColor", inactive);
            mesh.material.SetColor("_EmissionColor", inactive);
            transform.Translate(new Vector3(0f, 0.05f, 0f));
            OnDeactivate();
        }
        playersOnButton--;
        
    }
}

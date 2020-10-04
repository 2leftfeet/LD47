using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBroadcaster : MonoBehaviour
{
    public static event Action HasFinished;

    private void OnEnable()
    {
        HasFinished?.Invoke();
    }

    public void BroadcastSlamIntoDoor()
    {
        HasFinished?.Invoke();
    }
}

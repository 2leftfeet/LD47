using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineWrapper : MonoBehaviour
{
    public static CinemachineWrapper Instance => instance;
    private static CinemachineWrapper instance;

    private CinemachineVirtualCamera cvc;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            cvc = GetComponent<CinemachineVirtualCamera>();
        }
        else
        {
            Debug.LogError($"This is not okie dokie, two {GetType().ToString()} exist");
        }
    }

    public void AssignNewTarget(Transform newTrans)
    {
        cvc.Follow = newTrans;
    }
}

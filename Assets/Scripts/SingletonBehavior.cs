using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
    private void Awake()
    {
        instance = this.GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                return new GameObject(nameof(T)).AddComponent<T>();
            }
        }

        set
        {
            if(instance == null)
                instance = value;
            else
                throw new Exception("Assigning new singleton instance when one already exists");
        }
    }
}
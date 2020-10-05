using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class SingletonBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
    protected void Awake()
    {
        if (instance == null)
        {
            instance = this.GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
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
                T lookup = Object.FindObjectOfType<T>();
                if (lookup != null)
                    instance = lookup;
                else
                    instance = new GameObject(typeof(T).ToString()).AddComponent<T>();

                return instance;
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
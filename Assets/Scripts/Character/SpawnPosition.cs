using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    public static SpawnPosition Instance => instance;
    private static SpawnPosition instance;

    [SerializeField]
    private GameObject playerPrefab;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError($"This is not okie dokie, two {GetType().ToString()} exist");
        }
    }

    public GameObject SpawnPlayer()
    {
        return Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : SingletonBehavior<SpawnPosition>
{
    [SerializeField]
    private GameObject playerPrefab;

    public GameObject SpawnPlayer()
    {
        return Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }
}

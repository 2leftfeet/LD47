﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance => instance;
    private static LevelManager instance;

    [SerializeField] 
    private bool isDebug;
    
    [SerializeField]
    private SceneData[] levelData = null;
    private int currLevelIndex = 0;
    
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
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // LoadLevel(0);
        if(isDebug)
            StartLevel();
        else LoadLevel(0);
    }

    public void LoadNextLevel()
    {
        currLevelIndex++;
        LoadLevel(currLevelIndex);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(levelData[currLevelIndex].SceneName, LoadSceneMode.Single);
    }   

    public void TriggerVictory()
    {
        
    }
    
    public void TriggerDeath()
    {
        ResetLevel();
    }

    void StartLevel()
    {
        LoopManager.Instance.StartReplays();
        var go = SpawnPosition.Instance.SpawnPlayer();
        CinemachineWrapper.Instance.AssignNewTarget(go.transform);
    }

    void ResetLevel()
    {
        LoopManager.Instance.ResetReplays();
        StartLevel();
    }
}

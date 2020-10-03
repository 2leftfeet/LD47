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
    private LevelData[] levelData = null;
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
        LoadLevel(0);
    }

    private void LoadNextLevel()
    {
        currLevelIndex++;
        LoadLevel(currLevelIndex);
    }

    private void LoadLevel(int index)
    {
        SceneManager.LoadScene(levelData[currLevelIndex].SceneName, LoadSceneMode.Single);
    }

    public void TriggerVictory()
    {
        
    }
    
    public void TriggerDeath()
    {
        ResetScene();
    }

    void StartScene()
    {
        LoopManager.Instance.StartReplays();
    }

    void ResetScene()
    {
        LoopManager.Instance.ResetReplays();
        StartScene();
    }
}

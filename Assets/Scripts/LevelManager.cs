using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : SingletonBehavior<LevelManager>
{
    [SerializeField] 
    private bool isDebug;
    
    [SerializeField]
    private SceneData[] levelData = null;
    private int currLevelIndex = 0;
    
    private void Start()
    {
        // LoadLevel(0);
        if(isDebug)
            StartLevel();
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

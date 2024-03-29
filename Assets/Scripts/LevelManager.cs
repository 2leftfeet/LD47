﻿using System;
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

    private float rKeyHeldTime = 0;

    public AudioClip respawnAudio;
    [Range(0,1)]
    public float respawnAudioVolume;
    
    void Awake()
    {
        if (isDebug && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        base.Awake();
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        IntroBroadcaster.HasFinished += IntroBroadcasterOnHasFinished;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManagerOnsceneLoaded;
        IntroBroadcaster.HasFinished -= IntroBroadcasterOnHasFinished;
    }

    private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        
    }
    
    private void Start()
    {
        if(isDebug)
        {
            // Do nothing and it works :)
            // StartLevel();
        }
        else 
            LoadLevel(0);
    }

    public void LoadNextLevel()
    {
        currLevelIndex++;
        if (currLevelIndex >= levelData.Length)
            currLevelIndex = 0;
        
        LoadLevel(currLevelIndex);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(levelData[currLevelIndex].SceneName, LoadSceneMode.Single);
    }   

    public void TriggerVictory()
    {
        IEnumerator sequence = VictorySequence();
        StartCoroutine(sequence);
    }

    IEnumerator VictorySequence()
    {
        FadeManager.Instance.FadeOut(2f);
        yield return new WaitForSeconds(2f);
        AudioManager.StopMusic();
        LoadNextLevel();
        FadeManager.Instance.FadeIn(2f);
    }
    
    private void IntroBroadcasterOnHasFinished()
    {
        StartLevel();
        AudioManager.PlayMusic();
    }
    
    public void TriggerDeath()
    {
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(1.5f);
        AudioManager.PlayClip(respawnAudio,respawnAudioVolume);
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

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            rKeyHeldTime += Time.deltaTime;
        } 
        if(Input.GetKeyUp(KeyCode.R))
        {
            rKeyHeldTime = 0;
        }

        if (rKeyHeldTime >= 1.5f)
        {
            LoadLevel(currLevelIndex);
            rKeyHeldTime = 0;
        }
    }
}

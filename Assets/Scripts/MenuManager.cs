using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void LoadLevel(int index)
    {
        LevelManager.Instance.LoadNextLevel();
    }

    public void QuitLevel()
    {
         Application.Quit();
    }
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    [SerializeField]
    private string sceneName = null;
    public string SceneName => sceneName;
}

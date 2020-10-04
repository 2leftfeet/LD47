using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : SingletonBehavior<FadeManager>
{
    [SerializeField]
    private Image image;

    private float targetAlpha;
    private float seconds;
    
    private void Start()
    {
        FadeIn(5);
    }

    private void Update()
    {
        image.color = new Color(0,0,0,Mathf.MoveTowards(image.color.a, targetAlpha,Time.unscaledDeltaTime / seconds));
    }

    /// <summary>
    /// Make stuff visible
    /// </summary>
    /// <param name="s"></param>
    public void FadeIn(float s)
    {
        targetAlpha = 0;
        seconds = s;
    }

    /// <summary>
    /// Hide it all lmao
    /// </summary>
    /// <param name="s"></param>
    public void FadeOut(float s)
    {
        targetAlpha = 1;
        seconds = s;
    }
}

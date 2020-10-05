using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class LogicGateControler : MonoBehaviour
{

    private void OnEnable()
    {
        LoopManager.StartReplay += Reset;
    }

    [Header("Root options")]

    [SerializeField]
    bool useTranslate = false;
    [SerializeField]
    Button[] ButtonLisener;
    [SerializeField]
    bool doesReset = false;

    [Header("Translate options")]

    Transform startPoint;
    Vector3 makaroniPosition;
    [SerializeField] Transform endPoint;
    float speed;
    [SerializeField] float cycleTimeScaled = 2f;

    int totalActive = 0;
    int totalPlates;

    [Header("Animation Options")]
    
    [SerializeField]
    bool useTimer = false;
    [SerializeField]
    string animationToOpen = "";
    [SerializeField]
    string animationToClose = "";

    new Animator animation;


    [SerializeField]
    float resetTimer = 1f;
    bool activated = false;
    bool activeCounter = false;
    bool starPositionWasSet = false;

    

    private float pointInTime = 0f;

    private void Awake()
    {
        if (!startPoint)
        {
            
            makaroniPosition = gameObject.transform.position;
        }
        else
        {
            makaroniPosition = startPoint.position;
        }
        if (useTranslate)
        {
            if (endPoint)
                speed = Vector3.Distance(makaroniPosition, endPoint.position) / cycleTimeScaled;
            else
            {
                Debug.LogError("no end point set");
                endPoint = gameObject.transform;
            }
            useTimer = false;
        }

        animation = GetComponent<Animator>();

        totalPlates = ButtonLisener.Length;

        foreach (Button e in ButtonLisener)
        {
            e.OnActivate += CountActive;
            e.OnDeactivate += CountDeactive;
        }
    }
    
    private void Update()
    {
        if (totalActive >= totalPlates)
        {
            
            if (useTranslate)
            {
                MoveObject(true);
            }
            else
            {
                if (!activated)
                {
                    animation.SetTrigger(animationToOpen);
                }
                activated = true;
                if (useTimer)
                {
                    if (doesReset && !activeCounter)
                    {
                        activeCounter = true;
                        StartCoroutine(ResetTimer(resetTimer));
                    }
                }
            }
        }

        if(!useTimer && totalActive != totalPlates)
        {
            if (useTranslate && doesReset)
            {
                MoveObject(false);
            }
            else
            {
                if (activated)
                {
                    animation.SetTrigger(animationToClose);
                    activated = false;
                }
            }
        }
    }


    void CountActive() {
        totalActive++;
    }


    void CountDeactive() {
        totalActive--;
    }

    IEnumerator ResetTimer(float time)
    {
        yield return new WaitForSeconds(time);
        if (totalActive != totalPlates)
        {
            activated = false;
            animation.SetTrigger(animationToClose);
        }
        else
        {
            ResetTimer(time);
        }
        activeCounter = false;
    }





    private void MoveObject(bool forward)
    {
        if (forward && pointInTime < 1f)
        {
            pointInTime += (Time.deltaTime * speed );
            transform.position = Vector3.Lerp(makaroniPosition, endPoint.position, pointInTime);
        }
        if(!forward && pointInTime > 0f)
        {
            
            pointInTime -= (Time.deltaTime * speed);
            transform.position = Vector3.Lerp(makaroniPosition, endPoint.position, pointInTime);

        }
    }

    void Reset()
    {
        pointInTime = 0f;
        gameObject.transform.position = makaroniPosition;
    }

}
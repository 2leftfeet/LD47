﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{

    public GameObject target;
    public GameObject FOVpivot;
    private Vector2 targetPosition;
    private Vector2 prevPosition;

    private IEnumerator coroutine;

    private bool moving = false;
    private bool movingBack = false;

    Animator animator;
    private float minDistance = 0.25f;

    public float delay = 5f;
    public float speed = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        prevPosition = transform.position;
        coroutine = WanderWithDelay(delay);
		StartCoroutine(coroutine);
    }

    IEnumerator WanderWithDelay(float delay) {
        yield return new WaitForSeconds (delay);
        while (true) {
            if(movingBack){
                targetPosition = prevPosition;
                transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
                FOVpivot.transform.eulerAngles = new Vector3(FOVpivot.transform.eulerAngles.x, FOVpivot.transform.eulerAngles.y, FOVpivot.transform.eulerAngles.z + 180);
            } else {
                targetPosition = target.transform.position;
                transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
                FOVpivot.transform.eulerAngles = new Vector3(FOVpivot.transform.eulerAngles.x, FOVpivot.transform.eulerAngles.y, FOVpivot.transform.eulerAngles.z - 180);
            }
            animator.SetBool("Running",  true);
            moving = true;
            yield return new WaitForSeconds (delay);
            movingBack = !movingBack;
        }
    }

    void FixedUpdate()
    {
        if(moving){
            if(Vector2.Distance(transform.position, targetPosition) > minDistance)
                {
                    Vector2 pos = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
                    transform.position = new Vector3(pos.x, pos.y, transform.position.y);
                }
            else {
                animator.SetBool("Running",  false);
                transform.position = targetPosition;
                moving = false;
            }
        }
    }
}

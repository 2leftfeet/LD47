using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{

    public GameObject target;
    public GameObject FOVpivot;
    private Vector2 targetPosition;
    private Vector2 prevPosition;
    private Vector2 initialTargetPosition;
    private Quaternion initialPivotRot;

    private Vector3 startScale;

    private IEnumerator coroutine;

    private bool moving = false;
    private bool movingBack = false;
    private bool breakCoroutine = false;

    Animator animator;
    private float minDistance = 0.5f;

    public float delay = 5f;
    float delayTimer = 0.0f;
    public float speed = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        startScale = transform.localScale;
        prevPosition = transform.position;
        initialTargetPosition = target.transform.position; 
        initialPivotRot = FOVpivot.transform.rotation;
    }

    void Update()
    {
        if(delayTimer > delay)
        {
            WanderWithDelay();
            delayTimer = 0.0f;
        }
        delayTimer += Time.deltaTime;
    }

    void WanderWithDelay() {
        
        
        if(movingBack){
            targetPosition = prevPosition;
            transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
            FOVpivot.transform.eulerAngles = new Vector3(FOVpivot.transform.eulerAngles.x, FOVpivot.transform.eulerAngles.y + 180, FOVpivot.transform.eulerAngles.z);
        } 
        else{
            targetPosition = initialTargetPosition;
            transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
            FOVpivot.transform.eulerAngles = new Vector3(FOVpivot.transform.eulerAngles.x, FOVpivot.transform.eulerAngles.y - 180, FOVpivot.transform.eulerAngles.z);
        }
        animator.SetBool("Running",  true);
        moving = true;
        movingBack = !movingBack;
        
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

    public void Reset()
    {
        moving = false;
        movingBack = false;
        prevPosition = transform.position;
        if(animator)
            animator.SetBool("Running", false);
        transform.localScale = startScale;
        FOVpivot.transform.rotation = initialPivotRot;
        delayTimer = 0.0f;
    }
}

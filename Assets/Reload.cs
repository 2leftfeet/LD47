using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{

    public FieldOfView fov;
    Animator animator;
    public float reloadTime = 3f;
    float reloadTimer = 0.0f;
    IEnumerator coroutine;
 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ReloadQueue(){
        fov.enabled = false;
        animator.SetBool("Reloading", true);
        reloadTimer = 0.0f;
    }

    public void Reset()
    {
        reloadTimer = 0.0f;
        fov.enabled = true;
        animator.SetBool("Reloading", false);
    }
    // Update is called once per frame
    void Update()
    {
        if(fov.enabled == false && reloadTimer > reloadTime)
        {
            fov.enabled = true;
            animator.SetBool("Reloading", false);
            reloadTimer = 0.0f;
        }
        reloadTimer += Time.deltaTime;
    }
}

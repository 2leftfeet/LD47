using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonLookAtMouse : MonoBehaviour
{
    public GameObject projectile;
    public GameObject splashEffect;
    public Transform shootPoint;
    public Transform ikTarget;
    public Transform armRoot;
    public float distanceFromCharacter;
    Camera camera;

    public float scale;
    public int mousePosTrackingInterval = 10;
    public float reloadTime = 0.5f;
    float reloadTimer = 0.0f;
    int currFixedFrame = 0;

    [HideInInspector] public bool backwards;

    bool createShootAction = false;
    Vector3 mousePos;
    LoopTracker loopTracker;

    public AudioClip shootAudio;
    [Range(0,1)]
    public float audioVolume;


    void Awake()
    {
        camera = Camera.main;
        loopTracker = GetComponent<LoopTracker>();
    }


    void Update()
    {
        if(Input.GetMouseButton(0) && reloadTimer > reloadTime)
        {
            createShootAction = true;
            reloadTimer = 0.0f;
        }
        reloadTimer += Time.deltaTime;
        
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0.0f;

        LookAtPoint(mousePos);
        
    }

    void FixedUpdate()
    {
        if(createShootAction)
        {
            var action = new ShootAction(this, mousePos);
            loopTracker.RegisterAction(action);
            action.PlayAction();
            createShootAction = false;
            if (loopTracker.IsPlayerControlled){
                AudioManager.PlayClip(shootAudio,audioVolume,0.3f);
            }
            else AudioManager.PlayClip(shootAudio,0.15f,0.3f);
        }

        if(currFixedFrame % mousePosTrackingInterval == 0)
        {
            loopTracker.RegisterMousePos(mousePos);
            //Debug.Log("Registering mouse pos at fixed frame: " + currFixedFrame);
        }
        currFixedFrame++;
    }

    public void LookAtPoint(Vector3 mousePos)
    {
        Vector3 rootPos = new Vector3(transform.position.x, armRoot.position.y, 0.0f);
        Vector3 dir = (mousePos - rootPos).normalized;

        ikTarget.position = rootPos + dir * distanceFromCharacter;
 
        if(dir.x > 0)
        {
            transform.localScale = new Vector3(-scale, scale ,scale);
            backwards = true;
        }
        else
        {
            transform.localScale = new Vector3(scale, scale ,scale);
            backwards = false;
        }
    }

    public void ShootAtPoint(Vector3 mousePos)
    {
        mousePos.z = shootPoint.position.z;
        Vector3 dir = mousePos - shootPoint.position;
        Vector3 rotatedDir = Quaternion.Euler(0, 0, 90) * -dir;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, rotatedDir);
        var proj = Instantiate(projectile, shootPoint.position, rotation);
        if(!loopTracker.IsPlayerControlled){
            proj.gameObject.GetComponent<ProjectileMover>().projectileDestroyAudioVolume = 0.2f;
        }

        Instantiate(splashEffect, shootPoint.position, splashEffect.transform.rotation * rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonLookAtMouse : MonoBehaviour
{
    public Transform ikTarget;
    public Transform armRoot;
    public float distanceFromCharacter;
    public Camera camera;

    [HideInInspector] public bool backwards;

    void Update()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0.0f;
        Vector3 rootPos = new Vector3(transform.position.x, armRoot.position.y, 0.0f);
        Vector3 dir = (mousePos - rootPos).normalized;


        ikTarget.position = rootPos + dir * distanceFromCharacter;
 

        if(dir.x > 0)
        {
            transform.localScale = new Vector3(-1, 1 ,1);
            backwards = true;
        }
        else
        {
            transform.localScale = new Vector3(1, 1 ,1);
            backwards = false;
        }
    }
}

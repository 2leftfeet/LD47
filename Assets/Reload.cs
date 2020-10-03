using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{

    public FieldOfView fov;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ReloadQueue(){
        StartCoroutine("DisableScript");
    }

    IEnumerator DisableScript()
    {
        fov.enabled = false;

        yield return new WaitForSeconds(3f);

        fov.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

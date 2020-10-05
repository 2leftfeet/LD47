using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingBalancingStickposition : MonoBehaviour
{
    [SerializeField] Transform platformTolLookAt;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localRotation = Quaternion.Euler(0f, 0f,( Vector3.Angle(platformTolLookAt.position - gameObject.transform.position, Vector3.down)));
    }
}

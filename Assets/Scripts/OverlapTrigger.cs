using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapTrigger : MonoBehaviour
{
    public event System.Action OnTriggerEnter = delegate { };
    public event System.Action OnTriggerExit = delegate { };

    [SerializeField]
    LayerMask layer;// = LayerMask.GetMask("Default");

    [SerializeField]
    Vector3 positionOffset = new Vector3(0f,0f,0f);
    
    [SerializeField]
    float radius = 5.0f;

    [SerializeField]
    string targetTag = "Player";

    private ArrayList lastColliders = new ArrayList();
    
    // Update is called once per frame
    void FixedUpdate()
    { 

        ArrayList filteredColliders = new ArrayList();

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position + positionOffset, radius, layer);



        foreach (Collider entity in colliders)
        {
            bool same = false;
            if (entity.CompareTag(targetTag))
            {
                if (lastColliders.Count > 0)
                {
                    foreach (Collider old in lastColliders)
                    {
                        if (old == entity)
                        {
                            same = true;
                            break;
                        }
                    }
                }
                filteredColliders.Add(entity);
                if (!same)
                {
                    OnTriggerEnter();
                    Debug.Log("Enter");
                }
            }
        }

        foreach(Collider old in lastColliders)
        {
            bool foundSame = false;
            foreach(Collider newOne in filteredColliders)
            {
                if (old == newOne)
                {
                    foundSame = true;
                    break;
                }
            }
            if (!foundSame)
            {
                OnTriggerExit();
                Debug.Log("Exit");
            }
        }

        lastColliders.Clear();
        lastColliders = filteredColliders;

    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.green;
        
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

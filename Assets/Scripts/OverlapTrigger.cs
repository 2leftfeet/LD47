using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapTrigger : MonoBehaviour
{
    public event System.Action<Collider2D> OnTriggerEnter = delegate { };
    public event System.Action<Collider2D> OnTriggerExit = delegate { };
    public event System.Action<Collider2D> OnTriggerStay = delegate { };
    [SerializeField]
    LayerMask layer;// = LayerMask.GetMask("Everything");

    [SerializeField]
    Vector2 positionOffset = new Vector2(0f,0f);
    
    [SerializeField]
    float radius = 1.0f;

    [SerializeField]
    string targetTag = "Player";

    private ArrayList lastColliders = new ArrayList();


    // Update is called once per frame
    void FixedUpdate()
    { 

        ArrayList filteredColliders = new ArrayList();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(gameObject.transform.position.x,gameObject.transform.position.y) + positionOffset, radius, layer);



        foreach (Collider2D entity in colliders)
        {
            bool same = false;
            if (entity.CompareTag(targetTag))
            {
                if (lastColliders.Count > 0)
                {
                    foreach (Collider2D old in lastColliders)
                    {
                        if (old == entity)
                        {
                            same = true;
                            break;
                        }
                    }
                }
                filteredColliders.Add(entity);
                OnTriggerStay(entity);
                if (!same)
                {
                    OnTriggerEnter(entity);
                }
            }
        }

        foreach(Collider2D old in lastColliders)
        {
            bool foundSame = false;
            foreach(Collider2D newOne in filteredColliders)
            {
                if (old == newOne)
                {
                    foundSame = true;
                    break;
                }
            }
            if (!foundSame)
            {
                OnTriggerExit(old);
            }
        }

        lastColliders.Clear();
        lastColliders = filteredColliders;

    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.green;
        
        Gizmos.DrawWireSphere(transform.position + new Vector3(positionOffset.x, positionOffset.y, 0f), radius);
    }
}

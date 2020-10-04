using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapTrigger : MonoBehaviour
{
    
    public event System.Action<Collider2D> OnTriggerEnter = delegate { };
    public event System.Action<Collider2D> OnTriggerExit = delegate { };
    public event System.Action<Collider2D> OnTriggerStay = delegate { };

    private enum ColliderType
    {
        circle,
        box
    }

    [SerializeField]
    ColliderType colliderType = ColliderType.circle;

    [Header("Circle settings")]
    [SerializeField]
    float radius = 1.0f;

    [Header("Box settings")]

    [SerializeField]
    float sizeX = 1f;

    [SerializeField]
    float sizeY = 1f;

    [Header("Combined settings")]
    [SerializeField]
    LayerMask layer;// = LayerMask.GetMask("Everything");

    [SerializeField]
    Vector2 positionOffset = new Vector2(0f, 0f);

    [SerializeField]
    string targetTag = "Player";

    private ArrayList lastColliders = new ArrayList();


    // Update is called once per frame
    void FixedUpdate()
    { 

        ArrayList filteredColliders = new ArrayList();

        Collider2D[] colliders;

        if (colliderType == ColliderType.circle)
        {
            colliders = Physics2D.OverlapCircleAll(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + positionOffset, radius, layer);
        }
        else
        {
            colliders = Physics2D.OverlapBoxAll(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + positionOffset, new Vector2(sizeX, sizeY), 0f, layer);
        }

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
        if (colliderType == ColliderType.circle)
        {
            Gizmos.DrawWireSphere(transform.position + new Vector3(positionOffset.x, positionOffset.y, 0f), radius);
        }
        else
        {
            Gizmos.DrawWireCube(transform.position + new Vector3(positionOffset.x, positionOffset.y, 0f),new Vector3(sizeX, sizeY, 1f));
        }
        
    }
}

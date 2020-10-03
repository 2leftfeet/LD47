using System.Collections;
using System;
using UnityEngine;

struct RayOrigins{
    public Vector2 topLeft, topRight, bottomLeft, bottomRight;

    public Vector2 bottomLeftFalling, bottomRightFalling;
}

public struct CollisionState{
    public bool above, below, left, right;
}

[RequireComponent(typeof(BoxCollider2D))]
public class BodyMovement : MonoBehaviour
{
    [SerializeField] LayerMask groundLayermask = default;
    [SerializeField] float horizontalSkinWidthRising = 0.01f;
    [SerializeField] float horizontalSkinWidthFalling = 0.01f;
    [SerializeField] float verticalSkinWidth = 0.01f;
    [SerializeField] float horizontalRayCount = 3;
    [SerializeField] float verticalRayCount = 3;

    float horizontalRaySpacing;
    float verticalRaySpacingRising;
    float verticalRaySpacingFalling;

    BoxCollider2D box;
    RayOrigins rayOrigins;
    public CollisionState collisionState;
    public event Action OnGrounded = delegate{};
    
    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        GetRayOrigins();
        GetRaySpacing();
    }

     void GetRaySpacing()
    {
        Bounds bounds = box.bounds;

        var expandFactor = new Vector2(horizontalSkinWidthRising, verticalSkinWidth) * -2;
        bounds.Expand(expandFactor);

        horizontalRayCount = Mathf.Max(2, horizontalRayCount);
        verticalRayCount = Mathf.Max(2, verticalRayCount);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacingRising = bounds.size.x / (verticalRayCount - 1);

        bounds = box.bounds;

        expandFactor = new Vector2(horizontalSkinWidthFalling, verticalSkinWidth) * -2;
        bounds.Expand(expandFactor);

        verticalRaySpacingFalling = bounds.size.x / (verticalRayCount - 1);
    }

    void GetRayOrigins()
    {
        Bounds bounds = box.bounds;

        var expandFactor = new Vector2(horizontalSkinWidthRising, verticalSkinWidth) * -2;
        bounds.Expand(expandFactor);

        rayOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        rayOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        rayOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        rayOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);

        bounds = box.bounds;
        expandFactor = new Vector2(horizontalSkinWidthFalling, verticalSkinWidth) * -2;
        bounds.Expand(expandFactor);

        rayOrigins.bottomLeftFalling = new Vector2(bounds.min.x, bounds.min.y);
        rayOrigins.bottomRightFalling = new Vector2(bounds.max.x, bounds.min.y);
    }

    void CheckVerticalCollision(ref Vector2 vel)
    {
        collisionState.above = collisionState.below = false;
        //If were going up only ray from the top, if were going down, ray from the bottom
        float ySign = Mathf.Sign(vel.y);
        bool yPositive = ySign > 0.0f;
        Vector2 rayOrigin = yPositive ? rayOrigins.topLeft : rayOrigins.bottomLeftFalling;
        Vector2 rayDir = yPositive ? Vector2.up : Vector2.down;
        float rayLength = Mathf.Abs(vel.y) + verticalSkinWidth;

        float raySpacing = yPositive ? verticalRaySpacingRising : verticalRaySpacingFalling;

        float collisionDistance = float.MaxValue;
        bool collisionHappened = false;

        for (int i = 0; i < verticalRayCount; i++)
        {
            var hit = Physics2D.Raycast(rayOrigin + i * raySpacing * Vector2.right, rayDir, rayLength, groundLayermask);

            Debug.DrawRay(vel + rayOrigin + i * raySpacing * Vector2.right, rayDir * rayLength, Color.red);
            if(hit)
            {
                //vel.y = (hit.distance - verticalSkinWidth) * ySign;
                //rayLength = (hit.distance - verticalSkinWidth);
                collisionHappened = true;
                collisionDistance = Mathf.Min(hit.distance, collisionDistance);
            }
        }

        if(collisionHappened)
        {
            vel.y = (collisionDistance - verticalSkinWidth) * ySign;

            if(ySign > 0.0f)
                collisionState.above = true;
            else
                collisionState.below = true;
                OnGrounded();
        }
    }

    void CheckHorizontalCollision(ref Vector2 vel)
    {
        collisionState.left = collisionState.right = false;
        bool moveLeft = false;
        bool moveRight = false;

        if(Mathf.Abs(vel.x) < 0.0001f)
        {
            //If not moving horizontally, check collisions for both left and right
            moveLeft = moveRight = true;
        }
        else
        {
            //else only check one direction
            if(vel.x > 0.0f)
                moveRight = true;
            else
                moveLeft = true;
        }

        float rayLength = Mathf.Abs(vel.x) + horizontalSkinWidthRising;
        float collisionDistance = float.MaxValue;
        bool collisionHappened = false;
        float xSign = Mathf.Sign(vel.x);

        for(int i = 0; i < horizontalRayCount; i++)
        {
            if(moveRight){
                var hit = Physics2D.Raycast(rayOrigins.bottomRight + i * horizontalRaySpacing * Vector2.up, Vector2.right, rayLength, groundLayermask);
                Debug.DrawRay(vel + rayOrigins.bottomRight + i * horizontalRaySpacing * Vector2.up, Vector2.right * rayLength, Color.red);
                if(hit)
                {
                    collisionHappened = true;
                    collisionDistance = Mathf.Min(hit.distance, collisionDistance);
                    xSign = 1.0f;
                }

            }
            if(moveLeft)
            {
                var hit = Physics2D.Raycast(rayOrigins.bottomLeft + i * horizontalRaySpacing * Vector2.up, Vector2.left, rayLength, groundLayermask);
                Debug.DrawRay(vel + rayOrigins.bottomLeft + i * horizontalRaySpacing * Vector2.up, Vector2.left * rayLength, Color.red);
                if(hit)
                {
                    collisionHappened = true;
                    collisionDistance = Mathf.Min(hit.distance, collisionDistance);
                    xSign = -1.0f;
                }
            }
        }

        if(collisionHappened)
        {
            vel.x = (collisionDistance - horizontalSkinWidthRising) * xSign;

            if(xSign > 0.0f)
                    collisionState.right = true;
                else
                    collisionState.left = true;
        }
    }

    public void Move(Vector2 vel)
    {
        GetRayOrigins();
        CheckVerticalCollision(ref vel);
        CheckHorizontalCollision(ref vel);

        transform.Translate(vel);
    }
}
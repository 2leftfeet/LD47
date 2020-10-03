using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(BodyMovement))]
public class PhysicsController : MonoBehaviour
{
    //later either use Physics2D gravity or have a global data struct
    float gravity = -40f;
    float landingGravityModifier = 1.5f;
    float variableJumpGravityModifier = 0.6f;

    Vector2 velocity;
    Vector2 velocityBuffer;
    [HideInInspector] public BodyMovement body;

    [SerializeField] float runMaxSpeed = 5f;
    [SerializeField] float runAcceleration = 40f;
    [SerializeField] float jumpVelocity = 10.0f;
    [SerializeField] float jumpBufferTime = 0.1f;
    [SerializeField] float bangedStateTime = 0.5f;
    [SerializeField] float apexLowGravityVelocityRange = 2.0f;

    public float InputX {get; set;}
    public bool JumpPressed {get; set;}


    float jumpBuffer = 0.0f;
    
    void Awake()
    {
        body = GetComponent<BodyMovement>();
        InputX = 0.0f;
        JumpPressed = false;
    }

    void Update()
    {
        if(body.collisionState.above || body.collisionState.below)
        {
            velocity.y = 0.0f;
        }
        if(body.collisionState.left || body.collisionState.right)
        {
            velocity.x = 0.0f;
        }

        //Add gravity
        float lowGravityThreshold = apexLowGravityVelocityRange;
        if(velocity.y < -lowGravityThreshold)
        {
            velocity.y += gravity * landingGravityModifier * Time.deltaTime;
        }
        else
        {
            if(JumpPressed)
            {
                velocity.y += gravity * variableJumpGravityModifier * Time.deltaTime;
            }
            else
            {
                if(Math.Abs(velocity.y) < lowGravityThreshold)
                {
                    velocity.y += gravity * variableJumpGravityModifier * Time.deltaTime;
                }
                else
                {
                    velocity.y += gravity * Time.deltaTime;
                }
            }
        }
        
        if(JumpPressed)
        {
            jumpBuffer = jumpBufferTime;
        }

        if(jumpBuffer > 0.0f)
        {
            if(body.collisionState.below){
                velocity.y += jumpVelocity;
            }
            jumpBuffer -= Time.deltaTime;
        }

        //float targetX = Input.GetAxisRaw("Horizontal") * runMaxSpeed;
        float targetX = InputX * runMaxSpeed;

        float currentAcceleration = runAcceleration;
        velocity.x = Mathf.MoveTowards(velocity.x, targetX, currentAcceleration * Time.deltaTime);
        
        
        velocity += velocityBuffer;
        velocityBuffer = Vector2.zero;

        body.Move(velocity * Time.deltaTime);
    }

    public void AddVelocity(Vector2 addVel)
    {
        velocity.y = 0.0f;
        velocityBuffer = addVel;
    }
}
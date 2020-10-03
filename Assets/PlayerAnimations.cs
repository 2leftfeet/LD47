using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    PhysicsController player;

    void Awake()
    {
        player = GetComponent<PhysicsController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 velocity = player.GetVelocity();
        animator.SetFloat("Y_Velocity", velocity.y);

        animator.SetBool("Running", Mathf.Abs(velocity.x) > 0.0f);
        animator.SetBool("Jump", player.JumpPressed);
        animator.SetBool("Grounded", player.body.collisionState.below);
    }
}

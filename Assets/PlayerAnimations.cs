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
        animator.SetBool("Falling", velocity.y < 0.0f);

        animator.SetBool("Running", Mathf.Abs(velocity.x) > 0.0f);
        animator.SetBool("Jump", player.JumpPressed);
        animator.SetBool("Grounded", player.body.collisionState.below);
    }
}

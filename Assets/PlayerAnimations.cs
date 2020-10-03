using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    PhysicsController player;
    CannonLookAtMouse mouseAnim;

    void Awake()
    {
        player = GetComponent<PhysicsController>();
        animator = GetComponent<Animator>();
        mouseAnim = GetComponent<CannonLookAtMouse>();
    }

    void Update()
    {
        Vector2 velocity = player.GetVelocity();
        animator.SetBool("Falling", velocity.y < 0.0f);

        bool runningForwards = !(velocity.x > 0.0f ^ mouseAnim.backwards);

        if(Mathf.Abs(velocity.x) > 0.0f)
        {
            animator.SetBool("Running",  runningForwards);
            animator.SetBool("RunningBackwards", !runningForwards);
        }
        else
        {
            animator.SetBool("Running", false);
            animator.SetBool("RunningBackwards", false);
        }

        animator.SetBool("Jump", player.JumpPressed);
        animator.SetBool("Grounded", player.body.collisionState.below);
    }
}

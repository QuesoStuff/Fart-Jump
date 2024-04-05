using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    // Interface references
    public I_Moveable moveableBehavior_;
    public I_Jumpable jumpableBehavior_;
    public I_Jetpackable jetpackableBehavior_;
    public I_Healable_Player healBehavior_;
    public Rigidbody2D rb2d_;
    public static PlayerController instance_;

    private void Awake()
    {
        // Ensure singleton instance
        GENERIC.MakeSingleton(ref instance_, this, this.gameObject, true);
        ComponentUtility.AssignRigidbody2D(gameObject, out rb2d_);
        // Assign behaviors
        moveableBehavior_ = GetComponent<I_Moveable>();
        jumpableBehavior_ = GetComponent<I_Jumpable>();
        jetpackableBehavior_ = GetComponent<I_Jetpackable>();
        healBehavior_ = GetComponent<I_Healable_Player>();

        // If the components are not found on this GameObject, try finding them on children
        if (moveableBehavior_ == null) moveableBehavior_ = GetComponentInChildren<I_Moveable>();
        if (jumpableBehavior_ == null) jumpableBehavior_ = GetComponentInChildren<I_Jumpable>();
        if (jetpackableBehavior_ == null) jetpackableBehavior_ = GetComponentInChildren<I_Jetpackable>();
        if (healBehavior_ == null) healBehavior_ = GetComponentInChildren<I_Healable_Player>();
    }

    private void Update()
    {
        HandleMovement();
        HandleDash();
        HandleRefueling();
    }

    private void FixedUpdate()
    {
        // Instead of directly applying gravity, use the interface method
        jumpableBehavior_?.ApplyGravity();
        jetpackableBehavior_?.ApplyJetpackForce();
    }

    private void HandleMovement()
    {
        if (INPUT.Input_Move_Left()) moveableBehavior_?.MoveLeft();
        else if (INPUT.Input_Move_Right()) moveableBehavior_?.MoveRight();
        else if (INPUT.Input_Release_Left() || INPUT.Input_Release_Right()) moveableBehavior_?.StopMove();

        if (INPUT.Input_Tap_Jump() && jumpableBehavior_.IsGrounded)
        {
            jumpableBehavior_?.StartJump();
        }

        if (INPUT.Input_Release_Jump())
        {
            jumpableBehavior_?.StopJump();
        }
    }

    private void HandleDash()
    {
        if (INPUT.Input_Dash_Left()) moveableBehavior_?.DashLeft();
        else if (INPUT.Input_Dash_Right()) moveableBehavior_?.DashRight();
    }

    private void HandleRefueling()
    {
        // If not grounded and falling, refuel the jetpack
        if (!jumpableBehavior_.IsGrounded && rb2d_.velocity.y < 0)
        {
            jetpackableBehavior_?.RefuelJetpack();
        }
    }

    // Property implementations for IsGrounded, IsJumping, etc., based on the interface methods
    // ... existing property implementations ...

    // Collision handling for when the player collides with the ground
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpableBehavior_.IsGrounded = true;
            jumpableBehavior_.IsJumping = false;
            jetpackableBehavior_.JetpackActivated = false;
            healBehavior_.TakeFallDamage(rb2d_.velocity, () => Debug.Log("Player took fall damage."), () => Debug.Log("Player died from fall."));
            rb2d_.velocity = new Vector2(rb2d_.velocity.x, 0);
        }
    }

    // Called when the player exits a collision with the ground
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpableBehavior_.IsGrounded = false;
        }
    }
}

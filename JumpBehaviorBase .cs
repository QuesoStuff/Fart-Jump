using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class jumpBehavior_Base : MonoBehaviour, I_Jumpable
{
    protected Rigidbody2D rb2d_;
    [SerializeField]
    protected float gravityScale_ = 1f; // Default gravity scale
    [SerializeField]
    protected float jumpForce_ = 5f; // Default jump force
    protected bool isGrounded_;
    protected bool isJumping_;

    protected virtual void Awake()
    {
        ComponentUtility.AssignRigidbody2D(gameObject, out rb2d_);
    }

    public float GravityScale
    {
        get => gravityScale_;
        set
        {
            gravityScale_ = value;
            if (rb2d_ != null) rb2d_.gravityScale = gravityScale_;
        }
    }

    public bool IsGrounded
    {
        get => isGrounded_;
        set => isGrounded_ = value;
    }

    public bool IsJumping
    {
        get => isJumping_;
        set => isJumping_ = value;
    }

    public float JumpForce
    {
        get => jumpForce_;
        set => jumpForce_ = value;
    }

    public abstract void StartJump();
    public void ApplyGravity()
    {
        if (!isGrounded_)
        {
            // Explicit gravity application might not be needed if using Unity's physics,
            // but provided here for cases where custom gravity behavior is desired
            rb2d_.velocity += Vector2.down * gravityScale_ * Time.fixedDeltaTime;
        }
    }

    public void StopJump()
    {
        // Optionally reset vertical velocity
        rb2d_.velocity = new Vector2(rb2d_.velocity.x, 0);
        isJumping_ = false;
        isGrounded_ = true;
    }
}

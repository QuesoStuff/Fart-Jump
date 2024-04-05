using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpBehavior_Basic : jumpBehavior_Base
{
    public override void StartJump()
    {
        if (isGrounded_)
        {
            rb2d_.velocity = Vector2.up * jumpForce_;
            isGrounded_ = false; // Assume airborne until collision with ground detected
            isJumping_ = true;
        }
    }

}

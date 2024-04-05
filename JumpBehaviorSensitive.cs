using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class jumpBehavior_Sensitive : jumpBehavior_Base
{
    [SerializeField]
    private float maxJumpForce_ = 15f;  // The maximum force applied when jump button is held.
    [SerializeField]
    private float maxJumpTime_ = 0.2f;  // The maximum duration the jump force can be applied.

    // Properties for max jump force and max jump time.
    public float MaxJumpForce
    {
        get => maxJumpForce_;
        set => maxJumpForce_ = Mathf.Max(0, value); // Ensure the value is not negative.
    }

    public float MaxJumpTime
    {
        get => maxJumpTime_;
        set => maxJumpTime_ = Mathf.Max(0, value); // Ensure the value is not negative.
    }

    public override void StartJump()
    {
        if (isGrounded_)
        {
            StartCoroutine(JumpRoutine());
            isGrounded_ = false;
            isJumping_ = true;
        }
    }

    IEnumerator JumpRoutine()
    {
        while (INPUT.Input_Move_Jump() && (Time.time - INPUT.TimerInputPressJump <= maxJumpTime_))
        {
            float adjustedForce = Mathf.Lerp(jumpForce_, maxJumpForce_, (Time.time - INPUT.TimerInputPressJump) / maxJumpTime_);
            rb2d_.velocity = new Vector2(rb2d_.velocity.x, adjustedForce);
            yield return null;
        }

        isJumping_ = false;
    }

}

using System.Collections;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    // Rigidbody for player movement
    [SerializeField] private Rigidbody2D rb2d_;

    // Movement Parameters
    [Header("Movement Parameters")]
    [SerializeField] private float moveSpeed_ = 5.0f;
    [SerializeField] private float dashSpeed_ = 20f;
    [SerializeField] private float gravityScale_ = 25.0f;

    // Jumping Parameters
    [Header("Jumping Parameters")]
    [SerializeField] private float initialJumpForce_ = 0;
    [SerializeField] private float maxJumpForce_ = 15.0f;
    [SerializeField] private float maxJumpTime_ = 0.2f;

    // Jetpack Parameters
    [Header("Jetpack Parameters")]
    [SerializeField] private float jetpackForce_ = 2.0f;
    [SerializeField] private float jetpackFuel_ = 5.0f;
    [SerializeField] private float refuelRate_ = 0.5f; // Amount of fuel added per second while falling
    [SerializeField] private float maxJetpackFuel_ = 10.0f; // Maximum jetpack fuel

    // Dash Parameters
    [Header("Dash Parameters")]
    [SerializeField] private float dashDuration_ = 0.2f;

    // Internal State Flags
    private bool isGrounded_;
    private bool isJumping_;
    private bool jetpackActivated_;

    // Singleton instance
    public static Player_Move instance_;

    // Ensure singleton instance
    private void Awake()
    {
        GENERIC.MakeSingleton(ref instance_, this, this.gameObject, true);
    }

    // Getters and setters
    public bool IsGrounded { get => isGrounded_; set => isGrounded_ = value; }
    public bool IsJumping { get => isJumping_; set => isJumping_ = value; }
    public float JetpackFuel { get => jetpackFuel_; set => jetpackFuel_ = Mathf.Clamp(value, 0, maxJetpackFuel_); }
    public bool JetpackActivated { get => jetpackActivated_; set => jetpackActivated_ = value; }
    public float MaxJetpackFuel { get => maxJetpackFuel_; }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleDash();
        HandleRefueling();
        WrapAroundScreen();
    }

    // Physics-related updates
    private void FixedUpdate()
    {
        ApplyGravity();
        ApplyJetpack();
    }

    // Handles horizontal movement and jumping
    private void HandleMovement()
    {
        // Horizontal movement
        if (INPUT.Input_Move_Left()) MoveHorizontal(-moveSpeed_);
        else if (INPUT.Input_Move_Right()) MoveHorizontal(moveSpeed_);
        else if (INPUT.Input_Release_Left() || INPUT.Input_Release_Right()) StopMovingHorizontal();

        // Jump handling
        if (INPUT.Input_Tap_Jump())
        {
            if (IsGrounded)
                StartJump();
            else if (!IsGrounded && JetpackFuel > 0)
                JetpackActivated = true;
        }

        if (INPUT.Input_Release_Jump())
        {
            IsJumping = false;
            JetpackActivated = false;
        }
    }

    // Handles dash actions
    private void HandleDash()
    {
        int direction = 0;
        if (INPUT.Input_Dash_Left()) direction = -1;
        else if (INPUT.Input_Dash_Right()) direction = 1;
        if (direction != 0)
        {
            StartCoroutine(PerformDash(direction * dashSpeed_));
        }
    }

    // Coroutine for performing a dash
    IEnumerator PerformDash(float dashSpeed)
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration_)
        {
            rb2d_.velocity = new Vector2(dashSpeed, rb2d_.velocity.y);
            SpecialEffectsManager.instance_.CreateSimpleParticleEffect(0, transform.position + new Vector3(0f, 1f, 0f));
            yield return null;
        }
    }

    // Starts the jump process
    private void StartJump()
    {
        isGrounded_ = false;
        isJumping_ = true;
        INPUT.TimerInputPressJump = Time.time; // Set the jump timer at jump start
        rb2d_.velocity = new Vector2(rb2d_.velocity.x, initialJumpForce_);
        StartCoroutine(JumpHeightAdjustmentRoutine());
    }

    // Adjusts jump height based on input timing
    IEnumerator JumpHeightAdjustmentRoutine()
    {
        while (INPUT.Input_Move_Jump() && (Time.time - INPUT.TimerInputPressJump <= maxJumpTime_))
        {
            float adjustedForce = Mathf.Lerp(initialJumpForce_, maxJumpForce_, (Time.time - INPUT.TimerInputPressJump) / maxJumpTime_);
            rb2d_.velocity = new Vector2(rb2d_.velocity.x, adjustedForce);
            yield return null;
        }
    }

    // Applies gravity to the player
    private void ApplyGravity()
    {
        if (!IsGrounded) rb2d_.velocity += Vector2.down * gravityScale_ * Time.fixedDeltaTime;
    }

    // Applies jetpack force if the jetpack is activated and fuel is available
    private void ApplyJetpack()
    {
        if (JetpackActivated && JetpackFuel > 0)
        {
            rb2d_.velocity += new Vector2(0, jetpackForce_);
            JetpackFuel -= Time.fixedDeltaTime;
            if (JetpackFuel <= 0)
            {
                JetpackActivated = false;
                JetpackFuel = 0; // Ensure fuel doesn't go negative
            }
            SpecialEffectsManager.instance_.CreateSimpleParticleEffect(0, transform.position + new Vector3(0f, 1f, 0f));
        }
    }

    // Moves the player horizontally based on the speed
    private void MoveHorizontal(float speed)
    {
        rb2d_.velocity = new Vector2(speed, rb2d_.velocity.y);
    }

    // Stops horizontal movement
    private void StopMovingHorizontal()
    {
        rb2d_.velocity = new Vector2(0, rb2d_.velocity.y);
    }

    // Called when the player collides with the ground
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
            IsJumping = false;
            JetpackActivated = false;
            Player_Health.instance_.TakeFallDamage(rb2d_.velocity);
            rb2d_.velocity = new Vector2(rb2d_.velocity.x, 0);
        }
    }

    // Called when the player exits a collision with the ground
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
        }
    }

    // Refuels the jetpack if the player is falling
    private void HandleRefueling()
    {
        if (!IsGrounded && rb2d_.velocity.y < 0)
        {
            JetpackFuel = Mathf.Min(JetpackFuel + (refuelRate_ * Time.deltaTime), maxJetpackFuel_);
        }
    }

    // Wraps the player around the screen if they fall off
    private void WrapAroundScreen()
    {
        if (!GENERIC.IsObjectInView(transform))
        {
            Vector3 newPosition = new Vector3(0, 5, 0); // New position to move to if out of view
            GENERIC.RespawnAtNewLocation(transform, newPosition);
        }
    }
}

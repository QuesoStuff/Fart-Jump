public interface I_Jumpable
{
    bool IsGrounded { get; set; }
    bool IsJumping { get; set; }
    void StartJump();
    void ApplyGravity();
    void StopJump();
}
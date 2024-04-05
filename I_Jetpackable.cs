public interface I_Jetpackable
{
    float JetpackFuel { get; set; }
    bool JetpackActivated { get; set; }
    void ActivateJetpack();
    void ApplyJetpackForce();
    void RefuelJetpack();
    float MaxJetpackFuel { get; } // Add this line
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JetpackBehavior : MonoBehaviour, I_Jetpackable
{
    [SerializeField] private float jetpackForce_ = 2.0f;
    [SerializeField] private float maxJetpackFuel_ = 10.0f;
    [SerializeField] private float refuelRate_ = 0.5f;
    private float jetpackFuel_;
    private bool jetpackActivated_;
    private Rigidbody2D rb2d_;

    void Awake()
    {
        ComponentUtility.AssignRigidbody2D(gameObject, out rb2d_);
        jetpackFuel_ = maxJetpackFuel_; // Start with a full tank
    }

    public float JetpackForce
    {
        get => jetpackForce_;
        set => jetpackForce_ = value;
    }

    public float MaxJetpackFuel
    {
        get => maxJetpackFuel_;
        set => maxJetpackFuel_ = Mathf.Max(value, 0);
    }

    public float RefuelRate
    {
        get => refuelRate_;
        set => refuelRate_ = Mathf.Max(value, 0);
    }

    public float JetpackFuel
    {
        get => jetpackFuel_;
        set => jetpackFuel_ = Mathf.Clamp(value, 0, maxJetpackFuel_);
    }

    public bool JetpackActivated
    {
        get => jetpackActivated_;
        set => jetpackActivated_ = value;
    }

    public void ActivateJetpack()
    {
        if (jetpackFuel_ > 0)
        {
            rb2d_.velocity += Vector2.up * jetpackForce_ * Time.fixedDeltaTime;
            jetpackFuel_ -= Time.fixedDeltaTime;
            jetpackActivated_ = true; // Activate jetpack
            if (jetpackFuel_ <= 0)
            {
                jetpackFuel_ = 0;
                jetpackActivated_ = false; // Deactivate jetpack because fuel is depleted
            }
        }
        else
        {
            jetpackActivated_ = false; // Deactivate jetpack because there's no fuel
        }
    }

    public void ApplyJetpackForce()
    {
        if (JetpackActivated)
        {
            ActivateJetpack(); // Reuse ActivateJetpack for continuous application if needed
        }
    }

    public void RefuelJetpack()
    {
        if (!JetpackActivated) // Only refuel if the jetpack is not active
        {
            jetpackFuel_ = Mathf.Min(jetpackFuel_ + (refuelRate_ * Time.deltaTime), maxJetpackFuel_);
        }
    }
}

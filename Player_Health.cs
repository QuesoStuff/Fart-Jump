using UnityEngine;

public class Player_Health : MonoBehaviour
{
    // Static instance for singleton pattern
    public static Player_Health instance_;

    // Maximum HP of the player
    [SerializeField] private int maxHp_ = 100;
    [SerializeField] private int fallspeedDamage_ = 15;
    [SerializeField] private int fallDamage_ = 7;

    // Current HP of the player
    private int currentHp_;

    // Define delegates for healing, damages and death
    public delegate void HealHandler();
    public delegate void FullHealHandler();
    public delegate void DamageHandler();
    public delegate void PlayerDeathHandler();

    // Declare events of the delegate types
    public event HealHandler OnHeal__;
    public event HealHandler OnFullHeal__;
    public event DamageHandler OnDamage__;
    public event PlayerDeathHandler OnPlayerDeath_;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        GENERIC.MakeSingleton(ref instance_, this, this.gameObject, true);
        currentHp_ = maxHp_;
    }

    // Method to apply damage to the player
    public void TakeDamage(int damage)
    {
        currentHp_ -= damage;
        currentHp_ = Mathf.Max(currentHp_, 0); // Ensure HP doesn't go below 0
        // Invoke the death event if HP reaches 0
        if (currentHp_ <= 0)
        {
            OnPlayerDeath_?.Invoke();
        }
        else
        {
            OnDamage__?.Invoke();
        }
    }

    // Method to apply fall damage to the player
    public void TakeFallDamage(Vector2 velocity)
    {
        float currFallSpeed = Mathf.Abs(velocity.y);
        int damageMultiplier = (int)(currFallSpeed / fallspeedDamage_);
        int totalDamage = damageMultiplier * fallDamage_;
        TakeDamage(totalDamage);
    }

    // Method to heal the player
    public void Heal(int amount)
    {
        currentHp_ += amount;
        if (currentHp_ >= maxHp_)
            OnFullHeal__?.Invoke(); // Invoke Full heal event
        else
            OnHeal__?.Invoke(); // Invoke heal event
        currentHp_ = Mathf.Min(currentHp_, maxHp_); // Ensure HP doesn't exceed maxHp_
    }
    // Method of Full heal
    public void FullHeal()
    {
        currentHp_ = maxHp_;
        OnFullHeal__?.Invoke();
    }

    // Method to get the current HP of the player
    public int GetCurrentHp()
    {
        return currentHp_;
    }

    // Method to get the maximum HP of the player
    public int GetMaxHP()
    {
        return maxHp_;
    }
}

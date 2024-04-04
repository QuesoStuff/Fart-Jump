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

    // Define a delegate type for handling player death
    public delegate void PlayerDeathHandler();

    // Declare an event of the delegate type
    public event PlayerDeathHandler OnPlayerDeath;

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
            OnPlayerDeath?.Invoke();
        }
    }

    // fall damage 
    public void TakeFallDamage(Vector2 Velocity)
    {
        float currFallSpeed = Mathf.Abs(Velocity.y);
        Debug.Log("Current fall speed: " + currFallSpeed);

        int damageMultiplier = (int)(currFallSpeed / fallspeedDamage_);
        Debug.Log("Damage multiplier: " + damageMultiplier);

        TakeDamage(damageMultiplier * fallDamage_);
    }

    // Method to heal the player
    public void Heal(int amount)
    {
        currentHp_ += amount;
        currentHp_ = Mathf.Min(currentHp_, maxHp_); // Ensure HP doesn't exceed maxHp_
    }

    // Method to get the current HP of the player
    public int GetCurrentHp()
    {
        return currentHp_;
    }
    public int GetMaxHP()
    {
        return maxHp_;
    }

}

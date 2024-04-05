using System;

public interface I_Healable
{
    int CurrentHp { get; }
    int MaxHp { get; }

    // Now TakeDamage and Heal will accept Actions as parameters
    void TakeDamage(int damage, Action onDamage = null, Action onDeath = null);
    void Heal(int amount, Action onHeal = null, Action onFullHeal = null);
    void FullHeal(Action onFullHeal = null);
}

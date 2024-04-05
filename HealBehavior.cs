using UnityEngine;
using System;

public class HealBehavior : MonoBehaviour, I_Healable
{
    [SerializeField]
    private int maxHp = 100;
    private int currentHp;

    void Awake()
    {
        currentHp = maxHp;
    }

    public int CurrentHp => currentHp;
    public int MaxHp => maxHp;

    public void TakeDamage(int damage, Action onDamage = null, Action onDeath = null)
    {
        currentHp = Mathf.Max(currentHp - damage, 0);

        // Call the onDamage action if provided and damage taken
        if (damage > 0)
        {
            onDamage?.Invoke();
        }

        // If HP falls to zero and an onDeath action is provided, call it
        if (currentHp <= 0)
        {
            onDeath?.Invoke();
        }
    }

    public void Heal(int amount, Action onHeal = null, Action onFullHeal = null)
    {
        int previousHp = currentHp;
        currentHp = Mathf.Min(currentHp + amount, maxHp);

        // If HP was increased and an onHeal action is provided, call it
        if (currentHp > previousHp)
        {
            onHeal?.Invoke();
        }

        // If HP is now full and an onFullHeal action is provided, call it
        if (currentHp == maxHp)
        {
            onFullHeal?.Invoke();
        }
    }

    public void FullHeal(Action onFullHeal = null)
    {
        currentHp = maxHp;
        // If an onFullHeal action is provided, call it
        onFullHeal?.Invoke();
    }
}

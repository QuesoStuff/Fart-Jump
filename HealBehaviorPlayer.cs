using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealBehaviorPlayer : HealBehavior, I_Healable_Player
{
    [SerializeField] private int fallspeedDamage_ = 15;
    [SerializeField] private int fallDamage_ = 7;


    public void TakeFallDamage(Vector2 velocity, Action onDamage = null, Action onDeath = null)
    {
        float currFallSpeed = Mathf.Abs(velocity.y);
        int damageMultiplier = (int)(currFallSpeed / fallspeedDamage_);
        int totalDamage = damageMultiplier * fallDamage_;
        TakeDamage(totalDamage, onDamage, onDeath);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface I_Healable_Player : I_Healable
{
    void TakeFallDamage(Vector2 velocity, Action onDamage = null, Action onDeath = null);
}

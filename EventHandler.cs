using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private void OnEnable()
    {
        if (Player_Health.instance_ != null)
        {
            Player_Health.instance_.OnPlayerDeath_ += Event_PlayerDeath;
            Player_Health.instance_.OnHeal__ += Event_Heal;
            Player_Health.instance_.OnFullHeal__ += Event_FullHeal;
            Player_Health.instance_.OnDamage__ += Event_Damage;
        }
    }

    private void OnDisable()
    {
        if (Player_Health.instance_ != null)
        {
            Player_Health.instance_.OnPlayerDeath_ -= Event_PlayerDeath;
            Player_Health.instance_.OnHeal__ -= Event_Heal;
            Player_Health.instance_.OnFullHeal__ -= Event_FullHeal;
            Player_Health.instance_.OnDamage__ -= Event_Damage;
        }
    }

    private void Event_PlayerDeath()
    {
        Debug.Log("Game Over!");
    }

    private void Event_Heal()
    {
        Debug.Log("Player healed.");
    }

    private void Event_FullHeal()
    {
        Debug.Log("Player fully healed.");
    }

    private void Event_Damage()
    {
        Debug.Log("Player damaged.");
    }
}

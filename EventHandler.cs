using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private void OnEnable()
    {
        if (Player_Health.instance_ != null)
        {
            Player_Health.instance_.OnPlayerDeath += Event_PlayerDeath;
        }
    }

    private void OnDisable()
    {
        if (Player_Health.instance_ != null)
        {
            Player_Health.instance_.OnPlayerDeath -= Event_PlayerDeath;
        }
    }

    private void Event_PlayerDeath()
    {
        Debug.Log("Game Over!");
    }

}

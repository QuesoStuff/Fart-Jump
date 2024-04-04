using UnityEngine;
using UnityEngine.UI;

public class Debug_UI : MonoBehaviour
{
    [SerializeField] private Text debugText_;

    private void Update()
    {
        if (Player_Move.instance_ != null && Player_Health.instance_ != null && debugText_ != null)
        {
            Rigidbody2D rb = Player_Move.instance_.GetComponent<Rigidbody2D>();
            int maxHp = Player_Health.instance_.GetMaxHP();
            int currentHp = Player_Health.instance_.GetCurrentHp();
            float maxFuel = Player_Move.instance_.MaxJetpackFuel;
            float currentFuel = Player_Move.instance_.JetpackFuel;
            debugText_.text = $"HP: {currentHp} / {maxHp}\n" +
                             $"Position: {Player_Move.instance_.transform.position}\n" +
                             $"Velocity: {rb.velocity}\n" +
                             $"Jetpack Fuel: {currentFuel} / {maxFuel}";
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Debug_UI : MonoBehaviour
{
    [SerializeField] private Text debugText_;

    private void Update()
    {
        if (PlayerController.instance_ != null && debugText_ != null)
        {
            // Assuming PlayerController has references to I_Healable and I_Jetpackable interfaces
            I_Healable healBehavior = PlayerController.instance_.healBehavior_;
            I_Jetpackable jetpackBehavior = PlayerController.instance_.jetpackableBehavior_;

            // Access properties through the interfaces
            if (healBehavior != null && jetpackBehavior != null)
            {
                int maxHp = healBehavior.MaxHp;
                int currentHp = healBehavior.CurrentHp;
                float maxFuel = jetpackBehavior.MaxJetpackFuel; // Assuming you add a property for MaxJetpackFuel
                float currentFuel = jetpackBehavior.JetpackFuel;

                debugText_.text = $"HP: {currentHp} / {maxHp}\n" +
                                  $"Position: {PlayerController.instance_.transform.position}\n" +
                                  $"Velocity: {PlayerController.instance_.rb2d_.velocity}\n" + // Assuming rb2d_ is public or accessible
                                  $"Jetpack Fuel: {currentFuel} / {maxFuel}";
            }
        }
    }
}

using UnityEngine;

public class Debug_Particles : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0, 2, 0); // Offset for the explosion position

    // Update is called once per frame
    void Update()
    {
        // Check if the SpecialEffectsManager instance is available
        if (SpecialEffectsManager.instance_ == null) return;

        // Fetch the player's position as the base position for the explosion
        Vector3 basePosition = Player_Move.instance_.transform.position;

        // Check for each key press and trigger the corresponding explosion effect
        if (Input.GetKey(KeyCode.A))
        {
            SpecialEffectsManager.instance_.CreateParticleEffect(0, basePosition + offset, Color.white);
        }
        else if (Input.GetKey(KeyCode.B))
        {
            SpecialEffectsManager.instance_.CreateParticleEffect(1, basePosition + offset, Color.white);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            SpecialEffectsManager.instance_.CreateParticleEffect(2, basePosition + offset, Color.white);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            SpecialEffectsManager.instance_.CreateParticleEffect(3, basePosition + offset, Color.white);
        }
    }
}

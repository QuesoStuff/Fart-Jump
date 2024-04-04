using UnityEngine;

public class Test_ObjectFollower : MonoBehaviour
{
    [SerializeField] private Transform target; // Set this to the player's transform in the Inspector or via script
    [SerializeField] private Vector3 offset = new Vector3(0f, 1f, 0f); // Adjustable offset from the target
    [SerializeField] private float followSpeed = 5.0f; // Speed at which the object follows the target
    [SerializeField] private bool enableRespawn = true; // Whether to enable respawning near the target
    [SerializeField] private float respawnRange = 3.0f; // Range within which to respawn if enabled

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (target == null) return;

        if (enableRespawn)
        {
            // Example of respawning near the target with randomization
            if (!GENERIC.IsObjectInView(transform))
                GENERIC.RespawnWithOffsetAndRandomization(transform, target, offset, true, respawnRange);
        }
        else
        {
            // Directly following the target with a specified offset and speed
            Vector3 desiredPosition = target.position + offset;
            GENERIC.MoveTowardsTarget(transform, desiredPosition, followSpeed, isLerping: true);
        }
    }
}

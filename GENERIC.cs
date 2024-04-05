using System;
using UnityEngine;

public static class GENERIC
{
    // Validates double tap input
    public static bool ValidateDoubleTap(Func<bool> tapCondition, ref float timerInputPress, ref float lastDashTime, float dashCooldown, float doubleTapThreshold)
    {
        bool state = false;
        if (tapCondition())
        {
            if (Time.time - lastDashTime > dashCooldown && Time.time - timerInputPress < doubleTapThreshold)
            {
                lastDashTime = Time.time;
                state = true;
            }
            timerInputPress = Time.time;
        }
        return state;
    }

    // Creates a singleton instance of a MonoBehaviour
    public static void MakeSingleton<T>(ref T instance, T thisInstance, GameObject thisGameObject, bool persistAcrossScenes = true) where T : class
    {
        if (instance == null)
        {
            instance = thisInstance;
            if (thisGameObject.transform.parent != null)
            {
                thisGameObject.transform.SetParent(null, false);
            }
            if (persistAcrossScenes)
            {
                UnityEngine.Object.DontDestroyOnLoad(thisGameObject);
            }
        }
        else
        {
            UnityEngine.Object.Destroy(thisGameObject);
        }
    }

    // Moves an object towards a target position with specified speed
    public static void MoveTowardsTarget(Transform mover, Vector3 targetPosition, float speed, bool isLerping = false)
    {
        if (isLerping) // Smooth speed
        {
            mover.position = Vector3.Lerp(mover.position, targetPosition, speed * Time.deltaTime);
        }
        else // Fixed speed
        {
            mover.position = Vector3.MoveTowards(mover.position, targetPosition, speed * Time.deltaTime);
        }
    }

    // Checks if the object is within the camera's viewport
    public static bool IsObjectInView(Transform objectTransform)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(objectTransform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        return onScreen;
    }

    // Respawns the object with specified offset and optional randomization
    public static void RespawnWithOffsetAndRandomization(Transform objectToRespawn, Transform targetObject, Vector3 offset, bool randomizePosition, float randomRange)
    {
        Vector3 newPosition = targetObject.position + offset;
        if (randomizePosition)
        {
            float randomX = UnityEngine.Random.Range(-randomRange, randomRange);
            float randomY = UnityEngine.Random.Range(-randomRange, randomRange);
            newPosition += new Vector3(randomX, randomY, 0);
        }
        RespawnAtNewLocation(objectToRespawn, newPosition);
    }

    // Basic method for respawning an object at a new location
    public static void RespawnAtNewLocation(Transform objectToRespawn, Vector3 newPosition)
    {
        objectToRespawn.position = newPosition;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class GENERIC
{


    // Assuming doubleTapThreshold_ and dashCooldown_ are defined in INPUT or passed as parameters
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

    public static void MoveTowardsTarget(Transform mover, Vector3 targetPosition, float speed, bool isLerping = false)
    {
        if (isLerping) // smooth speed
        {
            mover.position = Vector3.Lerp(mover.position, targetPosition, speed * Time.deltaTime);
        }
        else //fixed speed
        {
            mover.position = Vector3.MoveTowards(mover.position, targetPosition, speed * Time.deltaTime);
        }
    }

    // Moves the object to a new location if it's out of the camera's view.


    // Checks if the object is within the camera's viewport.
    public static bool IsObjectInView(Transform objectTransform)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(objectTransform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        return onScreen;
    }

    // Respawns the object in relation to a target object, with an offset and optional randomization.
    public static void RespawnWithOffsetAndRandomization(Transform objectToRespawn, Transform targetObject, Vector3 offset, bool randomizePosition, float randomRange)
    {
        // Calculate the base new position with offset
        Vector3 newPosition = targetObject.position + offset;

        // If randomization is enabled, adjust the position within the specified range
        if (randomizePosition)
        {
            float randomX = UnityEngine.Random.Range(-randomRange, randomRange);
            float randomY = UnityEngine.Random.Range(-randomRange, randomRange);
            newPosition += new Vector3(randomX, randomY, 0);
        }

        // Use the basic respawn method to move the object
        RespawnAtNewLocation(objectToRespawn, newPosition);
    }

    // Basic method for respawning an object at a new location
    public static void RespawnAtNewLocation(Transform objectToRespawn, Vector3 newPosition)
    {
        objectToRespawn.position = newPosition;
    }
}

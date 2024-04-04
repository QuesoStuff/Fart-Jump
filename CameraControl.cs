using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform targetTransform_;
    [SerializeField] private float followSpeed_ = 5.0f;
    [SerializeField] private float xOffset_ = 0f;
    [SerializeField] private float yOffset_ = 0f;
    [SerializeField] private float zOffset_ = -10f;
    [SerializeField] private float zoom_ = 5f;
    private Camera camera_;

    private void Awake()
    {
        camera_ = GetComponent<Camera>();
    }
    private void Update()
    {
        CameraFollow();
        ApplyZoomInstantly(zoom_);
    }



    // Method to instantly apply the zoom level
    public void ApplyZoomInstantly(float newZoom)
    {
        if (camera_.orthographic)
        {
            camera_.orthographicSize = newZoom;
        }
        else
        {
            camera_.fieldOfView = newZoom;
        }
    }

    // Coroutine for smoothly transitioning the zoom level
    public IEnumerator ApplyZoomSmoothly(float targetZoom, float duration)
    {
        float time = 0;
        float startZoom = camera_.orthographic ? camera_.orthographicSize : camera_.fieldOfView;
        float endZoom = targetZoom;

        while (time < duration)
        {
            time += Time.deltaTime;
            float zoom = Mathf.Lerp(startZoom, endZoom, time / duration);

            if (camera_.orthographic)
            {
                camera_.orthographicSize = zoom;
            }
            else
            {
                camera_.fieldOfView = zoom;
            }

            yield return null;
        }

        // Ensure the zoom is exactly the targetZoom after the transition
        if (camera_.orthographic)
        {
            camera_.orthographicSize = targetZoom;
        }
        else
        {
            camera_.fieldOfView = targetZoom;
        }
    }


    public void CameraFollow()
    {
        Vector3 targetPosition = new Vector3(targetTransform_.position.x + xOffset_, targetTransform_.position.y + yOffset_, zOffset_);

        CameraFollowFixedSpeed(targetPosition);
        // CameraFollowTracking(targetPosition);
    }

    private void CameraFollowFixedSpeed(Vector3 targetPosition)
    {
        GENERIC.MoveTowardsTarget(transform, targetPosition, followSpeed_, isLerping: false);
    }

    private void CameraFollowTracking(Vector3 targetPosition)
    {
        GENERIC.MoveTowardsTarget(transform, targetPosition, followSpeed_, isLerping: true);
    }
}

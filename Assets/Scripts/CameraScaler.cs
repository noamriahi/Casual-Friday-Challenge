using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer backgroundRenderer;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        AdjustCameraSize();
    }

    private void AdjustCameraSize()
    {
        if (backgroundRenderer == null)
        {
            Debug.LogError("Assign a background sprite to the CameraResolutionFixer script!");
            return;
        }

        // Get the background's world size
        float bgHeight = backgroundRenderer.bounds.size.y;
        float bgWidth = backgroundRenderer.bounds.size.x;

        // Get screen aspect ratio
        float screenAspect = (float)Screen.width / Screen.height;
        float bgAspect = bgWidth / bgHeight;

        // Adjust camera size to ensure background fits
        if (screenAspect >= bgAspect)
        {
            cam.orthographicSize = bgHeight / 2f; // Adjust by height
        }
        else
        {
            cam.orthographicSize = (bgWidth / screenAspect) / 2f; // Adjust by width
        }

        Debug.Log($"Camera adjusted to orthographicSize: {cam.orthographicSize}");
    }
}

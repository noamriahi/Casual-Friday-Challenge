using UnityEngine;

/// <summary>
/// This script is made to scale the camera for any resolution
/// </summary>
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

        float bgHeight = backgroundRenderer.bounds.size.y;
        float bgWidth = backgroundRenderer.bounds.size.x;

        float screenAspect = (float)Screen.width / Screen.height;
        float bgAspect = bgWidth / bgHeight;

        if (screenAspect >= bgAspect)
        {
            cam.orthographicSize = bgHeight / 2f; 
        }
        else
        {
            cam.orthographicSize = (bgWidth / screenAspect) / 2f; 
        }
    }
}

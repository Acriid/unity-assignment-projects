using UnityEngine;

public class VisionCameraSetup : MonoBehaviour
{
    [SerializeField] private RenderTexture visionRT;
    [SerializeField] private float mapWorldSize = 16f; // match your tilemap world size

    void Awake()
    {
        var cam = GetComponent<Camera>();
        cam.orthographic       = true;
        cam.orthographicSize   = mapWorldSize / 2f;
        cam.nearClipPlane      = -10f; // 2D: negative near so it sees z=0 sprites
        cam.farClipPlane       = 10f;
        cam.clearFlags         = CameraClearFlags.SolidColor;
        cam.backgroundColor    = Color.black;
        cam.cullingMask        = LayerMask.GetMask("Vision Provider");
        cam.targetTexture      = visionRT;
        // Render before the main camera
        cam.depth              = -2;
    }
}

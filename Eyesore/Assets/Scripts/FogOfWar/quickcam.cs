using UnityEngine;

public class VisionCameraSetup : MonoBehaviour
{
    [SerializeField] private RenderTexture visionRT;
    [SerializeField] private float mapWorldSize = 16f; // match your tilemap world size

    void Awake()
    {
        visionRT.Release();
        visionRT.Create();

        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = visionRT;
        GL.Clear(false, true, Color.black);
        RenderTexture.active = prev;

        var cam = GetComponent<Camera>();
        cam.orthographic     = true;
        cam.orthographicSize = mapWorldSize / 2f;
        cam.nearClipPlane    = -10f;
        cam.farClipPlane     = 10f;
        cam.clearFlags       = CameraClearFlags.SolidColor;
        cam.backgroundColor  = Color.black;
        cam.cullingMask      = LayerMask.GetMask("Vision Provider");
        cam.targetTexture    = visionRT;
        cam.depth            = -2;

        // Force aspect to match the RT so world-to-UV math is correct
        cam.aspect = (float)visionRT.width / visionRT.height;
    }
}

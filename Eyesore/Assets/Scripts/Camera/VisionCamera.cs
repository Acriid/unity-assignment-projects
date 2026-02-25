using UnityEngine;

public class VisionCamera : MonoBehaviour
{
    public RenderTexture RenderTexture;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject visionCamGO = new GameObject("Vision Camera");
        Camera visionCam = visionCamGO.AddComponent<Camera>();
        visionCam.orthographic = true;
        visionCam.orthographicSize = 8f;
        visionCam.clearFlags = CameraClearFlags.SolidColor;
        visionCam.backgroundColor = Color.black;
        visionCam.cullingMask = LayerMask.GetMask("Vision Providers");
        visionCam.targetTexture = RenderTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

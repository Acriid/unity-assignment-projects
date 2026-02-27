using UnityEngine;

public class VisionCamera : MonoBehaviour
{
    public RenderTexture RenderTexture;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject visionCamGO = new GameObject("Vision Camera");
        Camera visionCam = visionCamGO.AddComponent<Camera>();
        visionCam.transform.position = new(0f,0f,-10f);
        visionCam.orthographic = true;
        visionCam.orthographicSize = 100f;
        visionCam.clearFlags = CameraClearFlags.SolidColor;
        visionCam.backgroundColor = Color.black;
        visionCam.cullingMask = LayerMask.GetMask("Vision Provider");
        visionCam.targetTexture = RenderTexture;
    }
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(10,10,256,256), RenderTexture);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

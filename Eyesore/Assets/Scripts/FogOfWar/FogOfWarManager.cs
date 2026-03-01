// FogOfWarManager.cs
using UnityEngine;

public class FogOfWarManager : MonoBehaviour
{
    [SerializeField] private Camera        mainCamera;
    [SerializeField] private Camera        visionCamera;
    [SerializeField] private Material      fogMaterial;
    [SerializeField] private RenderTexture visionRT;

    void LateUpdate()
    {
        // Main camera — use actual camera aspect (matches the screen)
        Vector2 mainPos = mainCamera.transform.position;
        fogMaterial.SetVector("_MainCamPos",    mainPos);
        fogMaterial.SetFloat ("_MainCamSize",   mainCamera.orthographicSize);
        fogMaterial.SetFloat ("_MainCamAspect", mainCamera.aspect);

        // Vision camera — derive aspect from the RT, not the camera component
        Vector2 visionPos    = visionCamera.transform.position;
        float   visionAspect = (float)visionRT.width / visionRT.height;
        fogMaterial.SetVector("_VisionCamPos",    visionPos);
        fogMaterial.SetFloat ("_VisionCamSize",   visionCamera.orthographicSize);
        fogMaterial.SetFloat ("_VisionCamAspect", visionAspect);
    }
}
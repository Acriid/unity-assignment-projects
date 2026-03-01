// FogOfWarManager.cs
using UnityEngine;

public class FogOfWarManager : MonoBehaviour
{
    [SerializeField] private Camera     visionCamera;
    [SerializeField] private Material   fogMaterial;

    void LateUpdate()
    {
        // Tell the fog shader where the vision camera's bounds are in world space
        // so it can convert screen UVs → vision texture UVs correctly.
        float size   = visionCamera.orthographicSize;
        Vector3 pos  = visionCamera.transform.position;

        // Pass centre + extents; sample in shader as:
        //   visionUV = (worldPos - _VisionCamPos.xy) / (_VisionCamSize * 2) + 0.5
        fogMaterial.SetVector("_VisionCamPos",  new Vector4(pos.x,  pos.y,  0, 0));
        fogMaterial.SetFloat ("_VisionCamSize", size);
    }
}
using UnityEngine;

public class ExploredFog : MonoBehaviour
{
    [SerializeField] private RenderTexture visionRT;
    [SerializeField] private RenderTexture exploredRT;
    private Material _accumulateMat;

    void Awake()
    {
        _accumulateMat = new Material(Shader.Find("Custom/ExploredFog"));

        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = exploredRT;
        GL.Clear(false, true, Color.black);
        RenderTexture.active = prev;
    }

    void LateUpdate()
    {
        Graphics.Blit(visionRT, exploredRT, _accumulateMat);
    }
}
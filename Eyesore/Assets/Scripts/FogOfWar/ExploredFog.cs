using UnityEngine;

public class ExploredFog : MonoBehaviour
{
    [SerializeField] private RenderTexture visionRT;
    [SerializeField] private RenderTexture exploredRT;
    private Material _accumulateMat;

// ExploredAccumulator.cs — Awake, no other changes needed
    void Awake()
    {
        _accumulateMat = new Material(Shader.Find("Custom/ExploredFog"));

        foreach (var rt in new[] { exploredRT, visionRT })
        {
            rt.Release();
            rt.Create();
            RenderTexture prev = RenderTexture.active;
            RenderTexture.active = rt;
            GL.Clear(false, true, Color.black);
            RenderTexture.active = prev;
        }
    }

    void LateUpdate()
    {
        Graphics.Blit(visionRT, exploredRT, _accumulateMat);
    }
}
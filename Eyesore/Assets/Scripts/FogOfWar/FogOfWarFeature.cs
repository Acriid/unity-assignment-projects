// FogOfWarFeature.cs
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;

public class FogOfWarFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public RenderTexture  visionRT;
        public RenderTexture  exploredRT;
        public Material       fogMaterial;          // uses FogOfWar.shader
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }

    public Settings settings = new();
    private FogOfWarPass _pass;

    public override void Create()
    {
        _pass = new FogOfWarPass(settings)
        {
            renderPassEvent = settings.renderPassEvent
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (settings.fogMaterial == null || settings.visionRT == null || settings.exploredRT == null)
            return;

        _pass.ConfigureInput(ScriptableRenderPassInput.Color);
        renderer.EnqueuePass(_pass);
    }

    // -------------------------------------------------------------------------
    class FogOfWarPass : ScriptableRenderPass
    {
        readonly Settings _s;
        public FogOfWarPass(Settings s) => _s = s;

        // All data the render graph lambda needs — no captured fields allowed
        class PassData
        {
            public TextureHandle  src;
            public Material       mat;
            public RenderTexture  visionRT;
            public RenderTexture  exploredRT;
        }

        class CopyBackData
        {
            public TextureHandle src;
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            var resourceData = frameData.Get<UniversalResourceData>();
            var cameraData   = frameData.Get<UniversalCameraData>();

            // Skip scene-view / preview cameras
            if (cameraData.isSceneViewCamera || cameraData.isPreviewCamera)
                return;

            TextureHandle cameraColor = resourceData.activeColorTexture;

            // Temporary texture with the same format/size as the camera target
            TextureDesc desc = renderGraph.GetTextureDesc(cameraColor);
            desc.name        = "_FogOfWarTemp";
            desc.clearBuffer = false;
            TextureHandle tempTex = renderGraph.CreateTexture(desc);

            // ------------------------------------------------------------------
            // Pass 1 — Blit camera → temp through the fog shader
            // ------------------------------------------------------------------
            using (var builder = renderGraph.AddRasterRenderPass<PassData>(
                       "FogOfWar_Apply", out var passData))
            {
                passData.src        = cameraColor;
                passData.mat        = _s.fogMaterial;
                passData.visionRT   = _s.visionRT;
                passData.exploredRT = _s.exploredRT;

                builder.UseTexture(cameraColor, AccessFlags.Read);
                builder.SetRenderAttachment(tempTex, 0, AccessFlags.Write);

                builder.SetRenderFunc(static (PassData data, RasterGraphContext ctx) =>
                {
                    data.mat.SetTexture("_VisionTex",   data.visionRT);
                    data.mat.SetTexture("_ExploredTex", data.exploredRT);

                    // scaleBias (1,1,0,0) = no scale/flip
                    Blitter.BlitTexture(ctx.cmd, data.src,
                                        new Vector4(1f, 1f, 0f, 0f), data.mat, 0);
                });
            }

            // ------------------------------------------------------------------
            // Pass 2 — Copy temp back to the camera colour target
            // ------------------------------------------------------------------
            using (var builder = renderGraph.AddRasterRenderPass<CopyBackData>(
                       "FogOfWar_CopyBack", out var passData))
            {
                passData.src = tempTex;

                builder.UseTexture(tempTex, AccessFlags.Read);
                builder.SetRenderAttachment(cameraColor, 0, AccessFlags.Write);

                builder.SetRenderFunc(static (CopyBackData data, RasterGraphContext ctx) =>
                {
                    Blitter.BlitTexture(ctx.cmd, data.src,
                                        new Vector4(1f, 1f, 0f, 0f), 0, false);
                });
            }
        }
    }
}
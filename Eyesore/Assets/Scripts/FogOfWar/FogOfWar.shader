Shader "Custom/FogOfWar"
{
    Properties
    {
        _VisionTex      ("Vision Mask",    2D)     = "black" {}
        _ExploredTex    ("Explored Mask",  2D)     = "black" {}
        _FogColor       ("Unexplored Fog", Color)  = (0.05, 0.05, 0.08, 1)
        _ExploredTint   ("Explored Tint",  Color)  = (0.25, 0.25, 0.30, 1)
        _ExploredDim    ("Explored Dim",   Range(0,1)) = 0.45
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex   Vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            TEXTURE2D(_VisionTex);   SAMPLER(sampler_VisionTex);
            TEXTURE2D(_ExploredTex); SAMPLER(sampler_ExploredTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _FogColor;
                float4 _ExploredTint;
                float  _ExploredDim;

                // Set every frame by FogOfWarManager.cs
                float2 _MainCamPos;
                float  _MainCamSize;        // orthographicSize (half-height in world units)
                float  _MainCamAspect;      // cam.aspect (width / height)

                float2 _VisionCamPos;
                float  _VisionCamSize;
                float  _VisionCamAspect;
            CBUFFER_END

            // Converts a screen UV (0..1) → world XY position
            // using the given orthographic camera parameters
            float2 ScreenUVToWorld(float2 uv, float2 camPos, float camSize, float camAspect)
            {
                // Remap uv from 0..1 to -1..1
                float2 ndc = uv * 2.0 - 1.0;
                // Scale by camera half-extents
                float2 worldOffset = ndc * float2(camSize * camAspect, camSize);
                return camPos + worldOffset;
            }

            // Converts a world XY position → UV in a given orthographic camera's render texture
            float2 WorldToVisionUV(float2 worldPos, float2 camPos, float camSize, float camAspect)
            {
                float2 offset = worldPos - camPos;
                float2 halfExtents = float2(camSize * camAspect, camSize);
                // Remap from [-halfExtents..halfExtents] to [0..1]
                return (offset / halfExtents) * 0.5 + 0.5;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 screenUV = IN.texcoord;

                // 1. Convert screen UV → world position (using main camera)
                float2 worldPos = ScreenUVToWorld(screenUV,
                                                  _MainCamPos,
                                                  _MainCamSize,
                                                  _MainCamAspect);

                // 2. Convert world position → vision texture UV (using vision camera)
                float2 visionUV = WorldToVisionUV(worldPos,
                                                  _VisionCamPos,
                                                  _VisionCamSize,
                                                  _VisionCamAspect);

                // Clamp so areas outside the vision camera's bounds stay black
                visionUV = saturate(visionUV);

                half4 scene    = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearClamp, screenUV);
                half  vision   = SAMPLE_TEXTURE2D(_VisionTex,   sampler_VisionTex,   visionUV).r;
                half  explored = SAMPLE_TEXTURE2D(_ExploredTex, sampler_ExploredTex, visionUV).r;

                if (vision > 0.1)
                    return scene;

                if (explored > 0.05)
                {
                    half  lum    = dot(scene.rgb, half3(0.299, 0.587, 0.114));
                    half3 grey   = half3(lum, lum, lum);
                    half3 dimmed = lerp(grey, _ExploredTint.rgb, 0.4) * _ExploredDim;
                    return half4(dimmed, 1.0);
                }

                return _FogColor;
            }
            ENDHLSL
        }
    }
}
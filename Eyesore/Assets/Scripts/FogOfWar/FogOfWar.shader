Shader "Custom/FogOfWar"
{
    Properties
    {
        _VisionTex    ("Vision Mask",    2D) = "black" {}
        _ExploredTex  ("Explored Mask",  2D) = "black" {}
        _FogColor     ("Unexplored Fog", Color) = (0.05, 0.05, 0.08, 1)
        _ExploredTint ("Explored Tint",  Color) = (0.25, 0.25, 0.30, 1)
        _ExploredDim  ("Explored Dim",   Range(0,1)) = 0.45
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex   Vert      // provided by Blit.hlsl
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            // Blit.hlsl provides: Vert, Varyings, _BlitTexture, sampler_LinearClamp, etc.
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"

            TEXTURE2D(_VisionTex);   SAMPLER(sampler_VisionTex);
            TEXTURE2D(_ExploredTex); SAMPLER(sampler_ExploredTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _FogColor;
                float4 _ExploredTint;
                float  _ExploredDim;
            CBUFFER_END

            half4 frag(Varyings IN) : SV_Target
            {
                float2 uv = IN.texcoord;

                // _BlitTexture is the camera colour — bound automatically by Blitter
                half4 scene    = SAMPLE_TEXTURE2D(_BlitTexture,  sampler_LinearClamp, uv);
                half  vision   = SAMPLE_TEXTURE2D(_VisionTex,   sampler_VisionTex,   uv).r;
                half  explored = SAMPLE_TEXTURE2D(_ExploredTex, sampler_ExploredTex, uv).r;

                // Currently visible
                if (vision > 0.1)
                    return scene;

                // Explored but not currently seen
                if (explored > 0.05)
                {
                    half  lum    = dot(scene.rgb, half3(0.299, 0.587, 0.114));
                    half3 grey   = half3(lum, lum, lum);
                    half3 dimmed = lerp(grey, _ExploredTint.rgb, 0.4) * _ExploredDim;
                    return half4(dimmed, 1.0);
                }

                // Never explored
                return _FogColor;
            }
            ENDHLSL
        }
    }
}
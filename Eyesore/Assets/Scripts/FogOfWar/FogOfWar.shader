Shader "Custom/FogOfWar"
{
    Properties
    {
        _MainTex      ("Screen Texture",  2D) = "white" {}
        _VisionTex    ("Vision Mask",     2D) = "black" {}
        _ExploredTex  ("Explored Mask",   2D) = "black" {}
        _FogColor     ("Unexplored Fog",  Color) = (0.05, 0.05, 0.08, 1)
        _ExploredTint ("Explored Tint",   Color) = (0.25, 0.25, 0.30, 1)
        _ExploredDim  ("Explored Dim",    Range(0,1)) = 0.45
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="Opaque" 
        }
        Cull Off
        ZWrite Off
        ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);     SAMPLER(sampler_MainTex);
            TEXTURE2D(_VisionTex);   SAMPLER(sampler_VisionTex);
            TEXTURE2D(_ExploredTex); SAMPLER(sampler_ExploredTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _FogColor;
                float4 _ExploredTint;
                float  _ExploredDim;
            CBUFFER_END

            struct appdata 
            { 
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0; 
            };
            struct v2f   
            { 
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata i)
            {
                v2f o = (v2f)0;
                o.positionCS = TransformObjectToHClip(i.positionOS.xyz);
                o.uv = TRANSFORM_TEX(i.uv, _MainTex);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 scene    = SAMPLE_TEXTURE2D(_MainTex,     sampler_MainTex,     i.uv);
                half  vision   = SAMPLE_TEXTURE2D(_VisionTex,   sampler_VisionTex,   i.uv).r;
                half  explored = SAMPLE_TEXTURE2D(_ExploredTex, sampler_ExploredTex, i.uv).r;

                // visable
                if (vision > 0.1)
                    return lerp(scene, scene, vision); 

                // explored
                if (explored > 0.05)
                {
                    half lum = dot(scene.rgb, half3(0.299, 0.587, 0.114));
                    half3 grey = half3(lum, lum, lum);
                    half3 dimmed = lerp(grey, _ExploredTint.rgb, 0.4) * _ExploredDim;
                    return half4(dimmed, 1.0);
                }

                // not explored
                return _FogColor;
            }
            ENDHLSL
        }
    }
}
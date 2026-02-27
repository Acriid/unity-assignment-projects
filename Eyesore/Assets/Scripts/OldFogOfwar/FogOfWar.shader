Shader "Custom/FogOfWar"
{
    Properties 
    {
        _VisionTex ("Vision Mask", 2D) = "black" {}
        _WorldSize ("World Size", Float) = 100

        _FogColor ("Fog Color", Color) = (0.15, 0.15, 0.18, 1)
        _ExploredColor ("Explored Gray", Color) = (0.3, 0.3, 0.35, 1)
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_VisionTex);
            SAMPLER(sampler_VisionTex);

            float _WorldSize;
            float4 _FogColor;
            float4 _ExploredColor;



            struct appdata
            {
                float4 positionOS : POSITION;
            };

            struct v2f
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o = (v2f)0;

                o.positionCS = TransformObjectToHClip(v.positionOS.xyz);
                o.positionWS = TransformObjectToWorld(v.positionOS.xyz);

                return o;
            };

            float4 frag(v2f i) : SV_TARGET
            {
                float2 uv;

                uv = i.positionWS.xy / _WorldSize;


                float mask = SAMPLE_TEXTURE2D(_VisionTex,sampler_VisionTex,uv).r;

                
                
                //return half4(uv,0,1);
                if(mask > 0.9)
                {
                    return float4(1,1,1,1);
                }
                else if(mask > 0.1)
                {
                    return _ExploredColor;
                }
                else
                {
                    return _FogColor;
                }

                //return half4(1,0,0,0.5);
            }

            ENDHLSL
        }
    }
}

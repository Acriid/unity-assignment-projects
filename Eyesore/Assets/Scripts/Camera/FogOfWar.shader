Shader "Custom/FogOfWar"
{
    Properties 
    {
        _MainTex ("Terrain Texture", 2D) = "white" {}
        _VisionTex ("Vision Mask", 2D) = "black" {}
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

            sampler2D _VisionTex;
            sampler2D _MainTex;

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

                uv.x = i.positionWS.x;
                uv.y = i.positionWS.y;


                float mask = tex2D(_VisionTex,uv).r;

                if(mask > 0.9)
                {
                    return float4(0,0,0,0);
                }
                else if(mask > 0.1)
                {
                    return _ExploredColor;
                }
                else
                {
                    return _FogColor;
                }
            }

            ENDHLSL
        }
    }
}

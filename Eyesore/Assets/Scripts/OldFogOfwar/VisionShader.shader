Shader "Custom/VisionCircle"
{
    Properties
    {
        _VisionRadius("Vision Radius",Float) = 5
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        Blend One One
        ZWrite Off
        ZTest Always
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            float4 _BaseColor;
            float _VisionRadius;

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
                float2 center = unity_ObjectToWorld._m03_m13;
                //float2 center = GetObjectToWorldMatrix()[3].xy;
                float dist = distance(i.positionWS.xy,center);


                float radius = _VisionRadius;
                half4 col = half4 (1,1,1,1);

                col.rgb *= saturate(1-dist/radius);
                
                return col;
            }

            ENDHLSL
        }
    }
}

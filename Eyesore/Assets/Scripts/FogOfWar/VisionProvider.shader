Shader "Custom/VisionProvider"
{
    Properties
    {
        _VisionRange ("Vision Range", Float) = 3.0
        _Softness    ("Edge Softness", Float) = 0.2
    }
    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent" 
            "RenderType"="Transparent"
        }
        //additive blend
        Blend One One
        ZWrite Off
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
            };
            //vertex to fragment
            struct v2f
            {
                float4 positionCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
            };

            CBUFFER_START(UnityPerMaterial)
                float _VisionRange;
                float _Softness;
            CBUFFER_END

            v2f vert(appdata i)
            {
                v2f o = (v2f)0;
                o.positionCS = TransformObjectToHClip(i.positionOS.xyz);
                
                o.uv = i.uv * 2.0 - 1.0;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                
                float dist = length(i.uv);
                
                float alpha = 1.0 - smoothstep(1.0 - _Softness, 1.0, dist);
                return half4(alpha, alpha, alpha, alpha);
            }
            ENDHLSL
        }
    }
}
Shader "Custom/SurfaceAngleSilhoetteOutline"
{
    HLSLINCLUDE
    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    sampler2D _MainTex;
    sampler2D _CameraDepthTexture;
    sampler2D _CameraGBufferTexture2;
    float4x4 UNITY_MATRIX_MVP;
    float4x4 _ViewProjectInverse;
    float _Thickness;
    float _Density;
    float4 _Color;

    struct FragInput
    {
        float4 vertex    : SV_Position;
        float2 texcoord  : TEXCOORD0;
        float3 cameraDir : TEXCOORD1;
    };

    FragInput VertMain(AttributesDefault v)
    {
        FragInput o;

        o.vertex = mul(UNITY_MATRIX_MVP, float4(v.vertex.xyz, 1.0));
        o.texcoord = TransformTriangleVertexToUV(v.vertex.xy);

        #if UNITY_UV_STARTS_AT_TOP
            o.texcoord = o.texcoord * float2(1.0, -1.0) + float2(0.0, 1.0);
        #endif

        float4 cameraForwardDir = mul(_ViewProjectInverse, float4(0.0, 0.0, 0.5, 1.0));
        cameraForwardDir.xyz /= cameraForwardDir.w;
        cameraForwardDir.xyz -= _WorldSpaceCameraPos;

        float4 cameraLocalDir = mul(_ViewProjectInverse, float4(o.texcoord.x * 2.0 - 1.0, o.texcoord.y * 2.0 - 1.0, 0.5, 1.0));
        cameraLocalDir.xyz /= cameraLocalDir.w;
        cameraLocalDir.xyz -= _WorldSpaceCameraPos;

        o.cameraDir = cameraLocalDir.xyz / length(cameraForwardDir.xyz);

        return o;
    }

    float4 FragMain(FragInput i) : SV_Target
    {
        float3 sceneColor = tex2D(_MainTex, i.texcoord).rgb;
        float  sceneDepth = tex2D(_CameraDepthTexture, i.texcoord).r;
        float3 sceneNormal = tex2D(_CameraGBufferTexture2, i.texcoord).xyz * 2.0 - 1.0;

        if (sceneDepth > 0.0)
        {
            float3 toCameraDir = normalize(-i.cameraDir);
            float silhouette = dot(toCameraDir, normalize(sceneNormal));

            sceneColor = float3(silhouette, silhouette, silhouette);
        }

        return float4(sceneColor, 1.0);
    }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
        HLSLPROGRAM
            #pragma vertex VertDefault
            #pragma fragment FragMain
        ENDHLSL
        }
    }
}
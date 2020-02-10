// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/WaterToon"
{
	Properties
	{
		[HDR] _Color("Color", Color) = (1, 1, 1, 1)
		_EdgeColor("Edge Color", Color) = (1, 1, 1, 1)
		[HDR] _AmbientColor("Ambient Color", Color) = (0.4 ,0.4 ,0.4 ,1)
		[HDR] _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
		_Glossiness("Glossiness", float) = 32
		_DepthFactor("Depth Factor", float) = 1.0
		_WaveSpeed("Wave Speed", float) = 1.0
		_NormalFactor("Normal Smoothing", float) = 1.0
		_DepthRampTex("Depth Ramp", 2D) = "white" {}
		_MainTex("Main Texture", 2D) = "white" {}
		_ReflectTex("Reflection Texture", 2D) = "white" {}
		_DistortStrength("Distort Strength", float) = 1.0
		_Transparency("Transparency", Range(0.0,1.0)) = 0.25
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}

		Pass
		{
			Tags
			{
				"LightMode" = "ForwardBase"
				"PassFlags" = "OnlyDirectional"
			}

			//Cull Back

			Lighting On
			SeparateSpecular On
			ZWrite Off

			// Alpha blending
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			#pragma vertex VertexShaderFunction
			#pragma fragment FragmentShaderFunction
			#pragma multi_compile_fog
			//#pragma multi_compile_fwdbase
			#pragma multi_compile_fwdadd_fullshadows

			uniform sampler2D _MainTex;
			uniform sampler2D _ReflectTex;
			float4 _Color;
			float4 _EdgeColor;
			float  _DepthFactor;
			float  _WaveSpeed;
			float  _WaveAmp;
			float _DistortStrength;
			sampler2D _CameraDepthTexture;
			sampler2D _DepthRampTex;
			float4 _AmbientColor;
			float _Transparency;
			float _Glossiness;
			float4 _SpecularColor;
			float _NormalFactor;

			struct vertexInput
			{
				float3 normal : NORMAL;
				float4 vertex : POSITION;
				float4 position : POSITION0;
				float4 uv : TEXCOORD0;
				float4 texCoord : TEXCOORD1;
			};

			struct vertexOutput
			{
				float3 worldNormal : NORMAL;
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
				float4 depth : TEXCOORD3;
			};

			// Vertex shader
			vertexOutput VertexShaderFunction(vertexInput input)
			{
				vertexOutput output;
				input.vertex.y = .50;
				float4 worldPos = mul(unity_ObjectToWorld, input.vertex);

				float displacement = sin(worldPos.x + (_WaveSpeed * _Time)) + sin(worldPos.z + (_WaveSpeed * _Time));
				worldPos.y = worldPos.y + (displacement * _DistortStrength);
				
				output.pos = mul(UNITY_MATRIX_VP, worldPos);
				output.worldNormal = UnityObjectToWorldNormal(input.normal);

				output.viewDir = WorldSpaceViewDir(input.vertex);

				// compute depth
				output.depth = ComputeScreenPos(output.pos);
				// texture coordinates 
				output.uv = input.texCoord;

				return output;
			}

			// Fragment shader
			float4 FragmentShaderFunction(vertexOutput input) : SV_Target
			{
				float3 normal = normalize(input.worldNormal);

				// Spec
				float3 viewDir = normalize(input.viewDir);
				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = dot(normal, halfVector);
				
				// apply depth texture
				float4 depthSample = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, input.depth);
				float depth = LinearEyeDepth(depthSample).r;

				// create foamline
				float foamLine = 1 - saturate(_DepthFactor * (depth - input.depth.w));
				float4 foamRamp = float4(tex2D(_DepthRampTex, float2(foamLine, 0.25)).rgb, 1.0);

				// sample main texture
				float4 albedo = tex2D(_MainTex, input.uv);
				float4 color =  _Color * foamRamp * albedo * _AmbientColor;

				color.a = _Transparency;

				return color;
			}
			ENDCG
		}
	}
}
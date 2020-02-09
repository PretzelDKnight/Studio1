Shader "Custom/OutlineShaderBump"
{
	Properties
	{
		_Color("Main Color", Color) = (.5,.5,.5,1)
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline width", Range(0.0, 0.5)) = .005
		_MainTex("Base (RGB)", 2D) = "white" { }
		_BumpMap("Bumpmap", 2D) = "bump" {}
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	struct appdata 
	{
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct vertexOutput 
	{
		float4 pos : POSITION;
		float4 color : COLOR;
	};

	uniform float _Outline;
	uniform float _Current;
	uniform float4 _OutlineColor;

	vertexOutput VertexOutputShader(appdata v) 
	{
		vertexOutput output;
		output.pos = UnityObjectToClipPos(v.vertex);

		float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 offset = TransformViewToProjection(normal.xy);

		output.pos.xy += offset * output.pos.z * _Outline * _Current;
		output.color = _OutlineColor;
		return output;
	}
	ENDCG

	SubShader
	{
		Tags { "Queue" = "Transparent" }

		Pass 
		{
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Off
			ZWrite Off
			ZTest Always

			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex VertexOutputShader
			#pragma fragment FragmentOutputShader
			
			half4 FragmentOutputShader(vertexOutput input) : COLOR 
			{
				return input.color;
			}

			ENDCG
		}
		
		
		CGPROGRAM
		#pragma surface surf Lambert
		struct Input
		{
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};

		sampler2D _MainTex;
		sampler2D _BumpMap;
		uniform float3 _Color;

		void surf(Input IN, inout SurfaceOutput output) 
		{
			output.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;
			output.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		}
		ENDCG
		
	}

	SubShader
	{
		Tags { "Queue" = "Transparent" }

		Pass 
		{
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite Off
			ZTest Always
			Offset 15,15

			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex VertexOutputShader
			#pragma exclude_renderers gles xbox360 ps3
			ENDCG

			SetTexture[_MainTex] 
			{ 
				combine primary 
			}
		}

		CGPROGRAM
		#pragma surface surf Lambert

		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};

		sampler2D _MainTex;
		sampler2D _BumpMap;
		uniform float3 _Color;

		void surf(Input IN, inout SurfaceOutput output) 
		{
			output.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;
			output.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
		}
		ENDCG
		
	}

	FallBack "Custom/OutlineShaderDiffused"
}
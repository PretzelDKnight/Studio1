Shader "Custom/OutlineShaderDiffused" 
{
	Properties
	{
		_Color("Main Color", Color) = (.5,.5,.5,1)
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline width", Range(0.0, 0.5)) = .005
		_MainTex("Base (RGB)", 2D) = "white" { }
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	struct appdata 
	{		
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct vertexOutput {
		float4 pos : POSITION;
		float4 color : COLOR;
	};

	uniform float _Outline;
	uniform float _Current;
	uniform float4 _OutlineColor;

	vertexOutput VertexOutputShader(appdata input)
	{
		vertexOutput output;
		output.pos = UnityObjectToClipPos(input.vertex);

		float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, input.normal);
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
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex VertexOutputShader
			#pragma fragment frag

			half4 frag(vertexOutput i) :COLOR 
			{
				return i.color;
			}

		ENDCG
		}

		Pass
		{
			Name "BASE"
			ZWrite On
			ZTest LEqual

			Blend SrcAlpha OneMinusSrcAlpha

			Material 
			{
				Diffuse[_Color]
				Ambient[_Color]
			}

			Lighting On

			SetTexture[_MainTex] 
			{
				ConstantColor[_Color]
				Combine texture * constant
			}

			SetTexture[_MainTex] 
			{
				Combine previous * primary DOUBLE
			}
		}
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
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex VertexOutputShader
			#pragma exclude_renderers gles xbox360 ps3
			ENDCG
			SetTexture[_MainTex] { combine primary }
		}

		Pass 
		{
			Name "BASE"
			ZWrite On
			ZTest LEqual

			Blend SrcAlpha OneMinusSrcAlpha

			Material 
			{
				Diffuse[_Color]
				Ambient[_Color]
			}

			Lighting On

			SetTexture[_MainTex] 
			{
				ConstantColor[_Color]
				Combine texture * constant
			}

			SetTexture[_MainTex] 
			{
				Combine previous * primary DOUBLE
			}
		}
	}

	Fallback "Diffuse"
}
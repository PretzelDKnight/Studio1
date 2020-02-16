Shader "Custom/Bloom" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
	}
	
	CGINCLUDE
		#include "UnityCG.cginc"

		sampler2D _MainTex;
		sampler2D _SourceTex;
		sampler2D _BloomMask;
		sampler2D _GlowMap;
		float4 _MainTex_TexelSize;
		half4 _Filter;
		half _Intensity;

		struct VertexData 
		{
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
		};

		struct Interpolators 
		{
			float4 pos : SV_POSITION;
			float2 uv : TEXCOORD0;
		};

		// simple sample function
		half3 Sample(float2 uv) 
		{
			return tex2D(_MainTex, uv).rgb;
		}

		// box kernel filter function
		half3 SampleBox(float2 uv, float delta) 
		{
			float4 o = _MainTex_TexelSize.xyxy * float2(-delta, delta).xxyy;
			half3 s = Sample(uv + o.xy) + Sample(uv + o.zy) + Sample(uv + o.xw) + Sample(uv + o.zw);
			return s * 0.25f;
		}

		// Prefilter step function for bloom threshold
		half3 Prefilter(half3 c) // using colors maximum component to determine brightness
		{
			half brightness = max(c.r, max(c.g, c.b));
			half soft = brightness - _Filter.y;
			soft = clamp(soft, 0, _Filter.z);
			soft = soft * soft * _Filter.w;
			half contribution = max(soft, brightness - _Filter.x);
			contribution /= max(brightness, 0.00001);
			return c * contribution;
		}

		Interpolators VertexProgram(VertexData v) 
		{
			Interpolators i;
			i.pos = UnityObjectToClipPos(v.vertex);
			i.uv = v.uv;
			return i;
		}
	ENDCG

	SubShader
	{
		Cull Off
		ZTest Always
		ZWrite Off

		Pass // pass number 0
		{
			CGPROGRAM
				#pragma vertex VertexProgram
				#pragma fragment FragmentProgram

				fixed4 FragmentProgram(Interpolators i) : SV_Target
				{
					return fixed4(Prefilter(SampleBox(i.uv, 1)), 1);
				}
			ENDCG
		}

		Pass // pass number 1
		{
			CGPROGRAM
				#pragma vertex VertexProgram
				#pragma fragment FragmentProgram

				fixed4 FragmentProgram(Interpolators i) : SV_Target
				{
					return fixed4(SampleBox(i.uv, 1), 1);
				}
			ENDCG
		}

		Pass // pass number 2
		{
			Blend One One

			CGPROGRAM
				#pragma vertex VertexProgram
				#pragma fragment FragmentProgram

				fixed4 FragmentProgram(Interpolators i) : SV_Target
				{
					return fixed4(SampleBox(i.uv, 0.5), 1);
				}
			ENDCG
		}

		Pass // pass number 3
		{
			CGPROGRAM
				#pragma vertex VertexProgram
				#pragma fragment FragmentProgram

				fixed4 FragmentProgram(Interpolators i) : SV_Target
				{
					fixed4 c = tex2D(_SourceTex, i.uv);
					c.rgb += _Intensity * SampleBox(i.uv, 0.5);
					return c;
				}
			ENDCG
		}

		Pass // pass number 4 (Debug pass)
		{
			CGPROGRAM
				#pragma vertex VertexProgram
				#pragma fragment FragmentProgram

				fixed4 FragmentProgram(Interpolators i) : SV_Target 
				{
					return tex2D(_MainTex, i.uv).rgba;
				}
			ENDCG
		}
	}
}
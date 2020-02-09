Shader "Custom/Toon"
{
	Properties
	{
		[HDR] _Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}
		_BlendAmount("Blend Amount", Range(0.0, 1.0)) = 1.0
		[HDR] _AmbientColor("Ambient Color", Color) = (0.4 ,0.4 ,0.4 ,1)
		_Intensity("Light Intensity", Range(0,1)) = 0.5
		_FallOff("Fall Off Rate", float) = 1
		[HDR] _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
		_Glossiness("Glossiness", float) = 32
		[HDR] _RimColor("Rim Color", Color) = (1,1,1,1)
		_RimAmount("Rim Amount", Range(0, 1)) = 0.716
		_RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
		_RiseAmount("Rise Amount", Range(-0.5, 0.5)) = 0.08
		_Shrink("Rise Shrink Factor", Range(-0.5, 0)) = -0.04
		_SelectAmount("Select Amount", Range(-0.5, 0.5)) = 0.3
	}

	SubShader
	{
		Pass
		{
			Tags
			{
				"LightMode" = "ForwardBase"
				"RenderType" = "Opaque"
				"PassFlags" = "OnlyDirectional"
			}

			CGPROGRAM
			#pragma vertex VertexShaderFunction
			#pragma fragment FragmentShaderFunction
			#pragma multi_compile_fog
			//#pragma multi_compile_fwdbase
			#pragma multi_compile_fwdadd_fullshadows

			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			uniform sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _AmbientColor;
			float _Glossiness;
			float4 _SpecularColor;
			float4 _RimColor;
			float _RimAmount;
			float _RimThreshold;
			float _Intensity;
			float4 _Color;
			float _RiseAmount;
			float _Moved;
			float _SelectAmount;
			float _Select;
			float height;
			float _FallOff;
			sampler2D _CameraDepthTexture;
			float _Shrink;
			int _Bool;

			struct appdata
			{
				float3 normal : NORMAL;
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
			};

			struct vertexOutput
			{
				float3 worldNormal : NORMAL;
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
				UNITY_FOG_COORDS(2)
				SHADOW_COORDS(3)
			};

			vertexOutput VertexShaderFunction(appdata input)
			{
				vertexOutput output;
				output.worldNormal = UnityObjectToWorldNormal(input.normal);

				output.pos = input.vertex;

				output.pos.xyz += output.pos.xyz * _Moved * _Shrink;

				output.pos = UnityObjectToClipPos(output.pos);

				// Vertex manipulations based on property
				output.pos.y -= _Moved * _RiseAmount;
				// Checking if relevant tile object is selected
				output.pos.y += _Bool > 0 ? _Select * _SelectAmount : 0;

				output.uv = TRANSFORM_TEX(input.uv, _MainTex);
				output.viewDir = WorldSpaceViewDir(input.vertex);
				UNITY_TRANSFER_FOG(output, output.pos);
				UNITY_TRANSFER_SHADOW(output, output.pos);
				return output;
			}

			float4 FragmentShaderFunction(vertexOutput input) : SV_Target
			{
				float3 normal = normalize(input.worldNormal);

				// Dot product of world normal and light
				float NdotL = dot(_WorldSpaceLightPos0, normal);

				// Getting shadow s n lights
				float shadow = SHADOW_ATTENUATION(input);

				// Making the light into bands
				//float lightIntensity = NdotL > 0 ? 1 : 0;
				float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);

				// Getting light color and adding it to the mix
				float4 light = lightIntensity * _LightColor0 * _Intensity;


				// xxxxxxxxxxxxxxxxxxxxxxx SPECULAR XXXXXXXXXXXXXXXXXXXXXX

				// Specular is dot product of normal of surface and falf vector (vector between viewing direction and light source)
				float3 viewDir = normalize(input.viewDir);
				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = dot(normal, halfVector);

				float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
				float4 specular = specularIntensitySmooth * _SpecularColor;


				// xxxxxxxxxxxxxxxxxxxxxx RIM xxxxxxxxxxxxxxxxxxxxxxxxxxxxx

				// Rim lighting
				float4 rimDot = 1 - dot(viewDir, normal);
				// making rim light only on the illuminated end of the object
				float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
				rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
				float4 rim = rimIntensity * _RimColor;


				// xxxxxxxxxxxxxxxxxxxxxx FOG xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

				// Sampling Texture
				float4 albedo = tex2D(_MainTex, input.uv);

				float4 Albedo = _Color * albedo * (_AmbientColor + light + specular + rim);

				UNITY_APPLY_FOG(input.fogCoord, Albedo);
				UNITY_OPAQUE_ALPHA(Albedo.a);

				return Albedo;
			}

			ENDCG
		}

		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}

	Fallback "Diffuse"
}

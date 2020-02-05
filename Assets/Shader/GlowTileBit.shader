Shader "Custom/ GlowTileBit"
{
    Properties
    {
        [HDR] _Color("Color", Color) = (0.5, 0.65, 1, 1)
        _MainTex("Color (RGB) Alpha (A)", 2D) = "white"
        _Frequency("Frequency", float) = 32
    }

    SubShader
    {
        Tags 
		{ 
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}

        LOD 100
		// Alpha blending
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex VertexShaderFunction
            #pragma fragment FragmentShaderFunction

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct vertexOutput
            {
                float2 uv : TEXCOORD0;
                float4 worldPos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _Color;
			float _Frequency;

			vertexOutput VertexShaderFunction(appdata input)
            {
				vertexOutput output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                output.worldPos = mul(unity_ObjectToWorld, input.vertex);
                return output;
            }

            float4 FragmentShaderFunction(vertexOutput input) : SV_Target
            {
                // sample the texture
                float4 albedo = tex2D(_MainTex, input.uv);

                // apply Color
				float4 color = _Color;

				//float blink = (1 + (sin(input.worldPos.x + (_Frequency * _Time)) + sin(input.worldPos.z + (_Frequency * _Time))) / 2) / 2;
                float blink = (1 + sin(input.worldPos.x + (_Frequency * _Time))) / 2;

				color.a = albedo.a;
				color.a = color.a * blink;

                return color;
            }
            ENDCG
        }
    }
}

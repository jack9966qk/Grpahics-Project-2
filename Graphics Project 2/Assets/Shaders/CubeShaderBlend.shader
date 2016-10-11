Shader "Unlit/CubeShaderBlend"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BlendFct ("Blend Factor", Float) = 0.5
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;	
			uniform float _BlendFct;

			struct vertIn
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			// Implementation of the vertex shader
			vertOut vert(vertIn v)
			{
				vertOut o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.uv = v.uv;
				return o;
			}
			
			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
				fixed4 col = (tex2D(_MainTex, v.uv) * _BlendFct) + (v.color * (1.0f - _BlendFct));
				return col;
			}
			ENDCG
		}
	}
}
